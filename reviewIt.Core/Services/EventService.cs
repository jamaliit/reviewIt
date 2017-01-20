using reviewIt.Core.ViewModels;
using reviewIt.Domain.Models;
using reviewIt.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Core.Services
{
    public class EventService
    {
        private reviewItContext db;
        private IRepository<Event> eventRepository;
        private Event events;

        public EventService()
        {
            db = new reviewItContext();
            eventRepository = new Repository<Event>(db);
            //<UserProfile>(db);
        }


        public void CreateEvent(EventViewModel eventVM)
        {
            events = new Event
            {
                EventId = eventVM.EventId,
                EventName = eventVM.EventName,
                EventDetails = eventVM.EventDetails,
                Location = eventVM.Location,
                Schedule = eventVM.Schedule,
            };

            eventRepository.Insert(events);
            db.SaveChanges();

        }

        public void UpdateEvent(EventViewModel eventVM)
        {
            events = new Event
            {
                EventId = eventVM.EventId,
                EventName = eventVM.EventName,
                EventDetails = eventVM.EventDetails,
                Location = eventVM.Location,
                Schedule = eventVM.Schedule,
            };

            eventRepository.Update(events);
            db.SaveChanges();
        }

        public void DeleteEvent(int eventID)
        {
            eventRepository.Delete(new Event { EventId = eventID });
            db.SaveChanges();
        }

        public IEnumerable<EventViewModel> GetAllEvent()
        {
            IEnumerable<EventViewModel> data = (from s in eventRepository.Get()
                                                //where s.IsAccount == true
                                                // join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                                select new EventViewModel
                                                {
                                                    EventId = s.EventId,
                                                    EventName = s.EventName,
                                                    EventDetails = s.EventDetails,
                                                    Location = s.Location,
                                                    Schedule = s.Schedule,

                                                }).AsEnumerable();
            return data;
        }


        public EventViewModel GetEventByID(int eventID)
        {
            EventViewModel data = (from s in eventRepository.Get()
                                   where s.EventId == eventID
                                   //join d in dropDownTypeRepository.Get() on s.DropDownTypeID equals d.DropDownTypeID
                                   select new EventViewModel
                                   {
                                       EventId = s.EventId,
                                       EventName = s.EventName,
                                       EventDetails = s.EventDetails,
                                       Location = s.Location,
                                       Schedule = s.Schedule,

                                   }).SingleOrDefault();
            return data;
        }
    }
}

