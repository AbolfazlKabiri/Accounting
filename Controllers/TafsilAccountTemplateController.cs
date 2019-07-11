using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    class TafsilAccountTemplateModelBindingAccountTemplateController:Repository.Repository
    {

        /// <summary>
        /// ایجاد الگوی جدید
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding InsertTafsilAccountTemplate(Models.TafsilAccountTemplateModelBinding model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_insertTafsilAccountTemplate"))
                {
                    repo.cmd.Parameters.AddWithValue("@entityId", model.EntityId);
                    repo.cmd.Parameters.AddWithValue("@tafsiliGroupId", model.TafsiliGroupId);
                    repo.cmd.Parameters.AddWithValue("@peopleGroupId", model.PeopleGroupId);
                    repo.ExecuteNonQuery();
                    return new Models.ActionResultModelBinding
                    {
                        Message = repo.rMsg,
                        Status = repo.rCode == 1 ? Models.ActionResult.Success : Models.ActionResult.Failed
                    };
                }
            }
            catch (Exception c)
            {
                return new Models.ActionResultModelBinding
                {
                    Message = c.Message,
                    Status = Models.ActionResult.Failed
                };
            }
        }
        /// <summary>
        /// ویرایش الگو
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding UpdateTafsilAccountTemplate(Models.TafsilAccountTemplateModelBinding model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_updateTafsilAccountTemplate"))
                {
                    repo.cmd.Parameters.AddWithValue("@id", model.Id);
                    repo.cmd.Parameters.AddWithValue("@entityId", model.EntityId);
                    repo.cmd.Parameters.AddWithValue("@tafsiliGroupId", model.TafsiliGroupId);
                    repo.cmd.Parameters.AddWithValue("@peopleGroupId", model.PeopleGroupId);
                    repo.ExecuteNonQuery();
                    return new Models.ActionResultModelBinding
                    {
                        Message = repo.rMsg,
                        Status = repo.rCode == 1 ? Models.ActionResult.Success : Models.ActionResult.Failed
                    };
                }
            }
            catch (Exception c)
            {
                return new Models.ActionResultModelBinding
                {
                    Message = c.Message,
                    Status = Models.ActionResult.Failed
                };
            }
        }
        /// <summary>
        /// حذف الگو
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding DeleteTafsilAccountTemplate(Models.TafsilAccountTemplateModelBinding model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_deleteTafsilAccountTemplate"))
                {
                    repo.cmd.Parameters.AddWithValue("@id", model.Id);
                    repo.ExecuteNonQuery();
                    return new Models.ActionResultModelBinding
                    {
                        Message = repo.rMsg,
                        Status = repo.rCode == 1 ? Models.ActionResult.Success : Models.ActionResult.Failed
                    };
                }
            }
            catch (Exception c)
            {
                return new Models.ActionResultModelBinding
                {
                    Message = c.Message,
                    Status = Models.ActionResult.Failed
                };
            }
        }
        /// <summary>
        /// دريافت ليست الگوهای تعرف شده
        /// </summary>
        /// <param name="_pageNo">
        /// شماره صفحه 
        /// </param>
        /// <param name="_seedNumber">
        /// تعداد ركوردهاي مدنظر براي مشاهده در خروجي
        /// </param>
        /// <returns></returns>
        public Models.SelectResultModelBinding<Models.TafsilAccountTemplateModelBinding> GetTafsilAccountTemplateList(short _pageNo = 0,short _seedNumber = 10,string search = null)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_getTafsilAccountTemplateList",true))
                {
                    pageNo = _pageNo;
                    seedNumber = _seedNumber;
                    
                    //repo.cmd.Parameters.AddWithValue("@search", search);
                    repo.ExecuteAdapter();
                    var info = repo.ds.Tables[0].AsEnumerable();
                    var TafsilAccountTemplateModelBindingiGroupList = repo.ds.Tables[1].AsEnumerable();
                    var peopleGroupList = repo.ds.Tables[2].AsEnumerable();
                    return new Models.SelectResultModelBinding<Models.TafsilAccountTemplateModelBinding>
                    {
                        Body = info.Select(i => new Models.TafsilAccountTemplateModelBinding()
                        {
                            Id = Convert.ToInt32(i.Field<object>("id")),
                            EntityId = Convert.ToInt32(i.Field<object>("entityTypeId")),
                            EntityTitle = Convert.ToString(i.Field<object>("entityTypeTitle")),
                            TafsiliGroupId = Convert.ToInt32(i.Field<object>("tafsiliGroupId")),
                            TafsiliGroupTitle = Convert.ToString(i.Field<object>("tafsiliGroupTitle")),
                            PeopleGroupId = Convert.ToInt32(i.Field<object>("peopleGroupId")),
                            PeopleGroupTitle = Convert.ToString(i.Field<object>("peopleGroupTitle")),
                            
                        }).ToList(),
                        TotalCount = repo.totalCount
                    };

                }
            }
            catch (Exception c)
            {
                System.Windows.MessageBox.Show(c.Message);
                return null;
            }
        }
    }
}
