using ContactsAPI.Models;

namespace ContactsAPI.Services
{
    public interface ISkillService
    {
        public Contact CreateContact(Contact contact);
        public Contact UpdateContact(Contact contact);
        public bool DeleteContact(int id);
        public Contact GetContact(int id);
        public List<Contact> GetContacts();


        public Skill CreateSkill(Skill skill);
        public Skill UpdateSkill(Skill Skill);
        public bool DeleteSkill(int id);
        public Skill GetSkill(int id);
        public List<Skill> GetSkills();

        public ContactSkill CreateContactSkill(Contact contact, Skill skill);
        public ContactSkill UpdateContactSkill(ContactSkill contactSkill);
        public bool DeleteContactSkill(int id);
        public List<ContactSkill> GetContactSkills(int contactId);
  
    }
}
