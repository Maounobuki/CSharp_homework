using NetStore.Models.DTO;

namespace NetStore.Abstraction
{
    public interface IGroupRepository
    {
        public int AddGroup(DTOGroup group);
        public string GetGroupsCSV();
        public string GetСacheStatCSV();

        public IEnumerable<DTOGroup> GetGroups();
    }
}
