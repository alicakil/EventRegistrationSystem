using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Models;
using System.Net;

namespace RestfulAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        Context c = new Context();


        [HttpGet("SignUp")]
        public Response SignUp(string NameSurname, string email, string phone)
        {
            // Validations...
            if (!string.IsNullOrEmpty(email) && !email.Contains("@"))
            {
                return new Response() { Result = false, Message = "The email adress you entered " + email + " is not valid!"};
            }

            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(phone))
            {
                return new Response() { Result = false, Message = "Please provide at least one contact information!" };  
            }

            // Transactions...
            c.Subscribers.Add(new Subscriber() { Name=NameSurname, Email=email, Phone = phone });
            c.SaveChanges();

            return new Response() { Result = true, Message = "You are succesfully signed up! Now you can register to any event." };
        }


        /// <summary>
        /// Will show you all events between 2 dates. A person is able to see the list of the events. Choosing one event from the list, he or she is able to         see the details of it, and register to it.
                /// </summary>
                /// <param name="StartDate"></param>
                /// <param name="EndDate"></param>
                /// <returns></returns>
        [HttpGet("ListOfEvents")]
        public IList<Event> ListOfEvents(DateTime StartDate, DateTime EndDate, int Page) // To list all events on the front-end side. (Mobile, Web Page anywhere..)
        {
            // Pagination used for performance. 100 records per page.
            Page--;
            return c.Events.Where(x => x.EventTime > StartDate && x.EventTime < EndDate).Skip(Page*100).Take(100).ToList();
        }

        [HttpGet("EventDetails")]
        public Event EventDetails(int eventid)
        {
            Event e = c.Events.Where(x => x.id == eventid).FirstOrDefault();
            if (e != null) e.GeoInformation = GetGeoInfo(e.Location);
            return e;
        }

        string GetGeoInfo(string Address)
        {
            string API_KEY = ""; // an api key needed here. this method gonna work in case a valid API Key provided.
            string url = @"https://maps.googleapis.com/maps/api/geocode/json?address=" + Address + "&key=" + API_KEY;

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);
            string responseFromServer = reader.ReadToEnd(); // json-formatted string from maps api
            response.Close();

            // response json needs to be parse here to return exact GPS Positions. But need an api_key first.

            return responseFromServer;
        }

        // This method is for Administrator
        [HttpGet("ListOfSubscribers")]
        public IList<Subscriber> ListOfSubscribers(int Page) // To list all events on the front-end side. (Mobile, Web Page anywhere..)
        {
            // Pagination used for performance. 100 records per page.
            Page--;
            return c.Subscribers.Skip(Page * 100).Take(100).ToList();
        }


        [HttpGet("RegisterToEvent")]
        public Response RegisterToEvent(int eventid, int subscriberid)
        {
            // Validations...
            if (!c.Events.Where(x=>x.id == eventid).Any())
            {
                return new Response() { Result = false, Message = "Event is not found!" };
            }

            if (!c.Subscribers.Where(x => x.id == subscriberid).Any())
            {
                return new Response() { Result = false, Message = "Subscriber is not found!" };
            }

            if (c.EventSubscribers.Where(x => x.Eventid==eventid && x.Subscriberid==subscriberid).Any())
            {
                return new Response() { Result = false, Message = "Ops! You are already registered for this event!" };
            }

            if (c.Events.Where(x => x.EventTime < DateTime.Now).Any())
            {
                return new Response() { Result = false, Message = "Ops! You are already registered for this event!" };
            }


            // Transactions...
            EventSubscriber es = new EventSubscriber() { Eventid = eventid, Subscriberid = subscriberid };
            c.EventSubscribers.Add(es);
            c.SaveChanges();

            return new Response() { Result = false, Message = "You are succesfully registered to the event! Your Ticket Number is : " + es.id };
        }




        public class Response
        {
            public bool Result { get; set; } // True : in case of an succesfull response to the front-end side. False : failed.
            public string Message { get; set; } // description from back-end side to the front-end side.
        }
    }
}
