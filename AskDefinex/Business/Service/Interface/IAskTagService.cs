using AskDefinex.Business.Model;
using DefineXwork.Library.Business;

namespace AskDefinex.Business.Service.Interface
{
    public interface IAskTagService : IService
    {
        public int CreateTag(TagCreateModel TagModel);
        public void UpdateTag(TagUpdateModel updateModel);
        public void DeleteTag(TagUpdateModel deleteModel);






    }
}
