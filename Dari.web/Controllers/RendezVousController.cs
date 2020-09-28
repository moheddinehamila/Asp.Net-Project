using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Dari.Data.Infrastructure;
using Dari.Domain;
using Dari.Service;
using System.IO;
using System.Threading;
using Microsoft.AspNet.Identity;
using static Dari.web.Models.ApplicationUser;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace Dari.web.Controllers
{
    public class RendezVousController : Controller
    {
        #region Declaration
        IDataBaseFactory dbf;
        IUnitOfWork uow;
        IRendezVous<RendezVous> serviceRdv;
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";
        public string strCurrentUserId { get; private set; }

        public RendezVousController()
        {
            dbf = new DataBaseFactory();
            uow = new UnitOfWork(dbf);
            serviceRdv = new ServiceRendezVous<RendezVous>(uow);
        }
        #endregion

        #region APIGoogleCalendar
        //Utilisation de l'API
        public List<RendezVous> ApiCalendar()
        {
            #region GetTheAutorisation
            UserCredential credential;
            var path = Server.MapPath(@"~/code_secret_client.json");

            using (var stream =
            new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.            
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                new[] { CalendarService.Scope.Calendar },
                "user",
                CancellationToken.None,

                //file to save the token key     
                new FileDataStore(Server.MapPath("~/App_Data"), true)).Result;
            }
            #endregion


            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 100;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            var events1 = new List<RendezVous>();
            List<RendezVous> lst = new List<RendezVous>();
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    String startdate = eventItem.Start.DateTime.ToString();
                    String enddate = eventItem.End.DateTime.ToString();
                    String titre = eventItem.Summary;
                    DateTime convertstartdate = DateTime.ParseExact(startdate, "dd/MM/yyyy HH:mm:ss", null);
                    DateTime convertenddate = DateTime.ParseExact(enddate, "dd/MM/yyyy HH:mm:ss", null);

                    events1.Add(new RendezVous()
                    {
                        start = convertstartdate.ToString("MM/dd/yyyy HH:mm:ss"),
                        end = convertenddate.ToString("MM/dd/yyyy HH:mm:ss"),
                        title = titre,
                        description = "From your Google Calendar"

                    });
                }
            }

            return (events1);

        }
        #endregion
        public ActionResult Viewrdv()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        #region CRUD
        // GET: /RendezVous/GetCalendarData
        
        public ActionResult GetCalendarData()
        {
            //list from API Google Calendar
            ApiCalendar();
            var events = new List<RendezVous>();
            List<RendezVous> lst = new List<RendezVous>();
            //lst = (serviceRdv.GetAll()).Cast<RendezVous>().ToList();
            strCurrentUserId = User.Identity.GetUserId();
            
            if (User.IsInRole("Client"))
            {

                lst = (serviceRdv.GetByIDUser(p => p.idbuyer.Equals(strCurrentUserId))).Cast<RendezVous>().ToList();
            }else
            {
                lst = (serviceRdv.GetByIDUser(p => p.idseller.Equals(strCurrentUserId))).Cast<RendezVous>().ToList();

            }
            int cnt = lst.Count;
            foreach (RendezVous r in lst)
            {
                //List From DB
                events.Add(r);
            }

            //Join the Two List
            events.AddRange(ApiCalendar());

            // Return info.
            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);

        }



        //le calendrier de l'utilisateur
        public ActionResult GetCalendarDatabyuser()
        {
            ApiCalendar();
            var events = new List<RendezVous>();
            List<RendezVous> lst = new List<RendezVous>();
            lst = (serviceRdv.GetAll()).Cast<RendezVous>().ToList();
          //  lst = (serviceRdv.GetByIDUser(p => p.idseller.Equals(User.Identity.GetUserId())).Cast<RendezVous>().ToList());
            int cnt = lst.Count;
            foreach (RendezVous r in lst)
            {
                events.Add(r);

            }

            //Join the Two List
            events.AddRange(ApiCalendar());

            // Return info.
            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);

        }



        //update function       
        public JsonResult UpdateEvent(RendezVous r)
        {
            //data de la partie js est envoyer dans le controller 
            //deviennt "r"

            strCurrentUserId = User.Identity.GetUserId();
            var status = false;
            if (r.id > 0)
            {
                var v = serviceRdv.GetById(r.id);
                if (v != null)
                {
                    v.title = r.title;
                    v.start = r.start;
                    v.end = r.end;
                    v.description = r.description;
                    v.color = r.color;
                    serviceRdv.Update(v);
                    serviceRdv.Commit();
                    status = true;
                }
            }
            else
            {
                serviceRdv.Add(r);
                serviceRdv.Commit();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }



        //add function for buyer ("acheteur") ou le client
        //la valeur par defau du couleur va etre orangé
        [Authorize(Roles = "Client")]
        public JsonResult AddEvent(RendezVous r)
        {
            var v = new RendezVous();
            var status = false;
            strCurrentUserId = User.Identity.GetUserId();
            v.title = r.title;
            v.start = r.start;
            v.end = r.end;
            v.description = r.description;
            v.color = "orange";
            v.idbuyer = strCurrentUserId;
            serviceRdv.Add(v);
            serviceRdv.Commit();
            status = true;


            return new JsonResult { Data = new { status = status } };
        }


        //fonction ajout pour le seller ("vendeur") ou le owner
        //il peut metre les valur de couleur (orange,red,green)
        [Authorize(Roles = "Owner")]
        public JsonResult AddEventSeller(RendezVous r)
        {
            var v = new RendezVous();
            var status = false;
            strCurrentUserId = User.Identity.GetUserId();
            v.title = r.title;
            v.start = r.start;
            v.end = r.end;
            v.description = r.description;
            v.color = r.color;
            v.idseller = strCurrentUserId;
            serviceRdv.Add(v);
            serviceRdv.Commit();
            status = true;


            return new JsonResult { Data = new { status = status } };
        }


        //Delete Function 
        public JsonResult DeleteEvent(int id)
        {

            var v = serviceRdv.GetById(id);
            var status = false;
            if (v != null)
            {
                serviceRdv.Delete(serviceRdv.GetById(id));
                serviceRdv.Commit();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };

        }

        #endregion
    }
}