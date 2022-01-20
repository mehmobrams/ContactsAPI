using ContactsAPI.Models;
using ContactsAPI.Repositories;

namespace ContactsAPI.Services
{
    public class SkillService : ISkillService
    {

        public Contact CreateContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Contact UpdateContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        public bool DeleteContact(int id)
        {
            throw new NotImplementedException();
        }

        public Contact GetContact(int id)
        {
            var contact = new Contact();
            //var contact = ContactsAPI.Controllers.ContactsController.FirstOrDefault(o => o.Id == id);

            if (contact is null) return null;

            return contact;
        }

        public List<Contact> GetContacts()
        {
            throw new NotImplementedException();
        }

        public Skill CreateSkill(Skill skill)
        {
            throw new NotImplementedException();
        }

        public Skill UpdateSkill(Skill Skill)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSkill(int id)
        {
            throw new NotImplementedException();
        }

        public Skill GetSkill(int id)
        {
            throw new NotImplementedException();
        }

        public List<Skill> GetSkills()
        {
            throw new NotImplementedException();
        }

        public ContactSkill CreateContactSkill(Contact contact, Skill skill)
        {
            throw new NotImplementedException();
        }

        public ContactSkill UpdateContactSkill(ContactSkill contactSkill)
        {
            throw new NotImplementedException();
        }

        public bool DeleteContactSkill(int id)
        {
            throw new NotImplementedException();
        }

        public List<ContactSkill> GetContactSkills(int contactId)
        {
            throw new NotImplementedException();
        }
    }
}
