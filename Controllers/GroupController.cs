using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using System.Data;

namespace Accounting.Controllers
{
    class GroupController:Repository.Repository
    {
        /// <summary>
        /// ثبت گروه حساب جديد
        /// </summary>
        /// <param name="groupModel">
        /// مدل پايه گروه حساب
        /// </param>
        /// <returns></returns>
        public Models.ActionResultModelBinding InsertGroup(Models.Group groupModel)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_insertAccountingGroup"))
                {
                    repo.cmd.Parameters.AddWithValue("@code", groupModel.Code);
                    repo.cmd.Parameters.AddWithValue("@title", groupModel.Title);
                    repo.cmd.Parameters.AddWithValue("@natureId", groupModel.NatureId);
                    repo.cmd.Parameters.AddWithValue("@isDefault", groupModel.IsDefault);
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
        /// دريافت ليست گروه هاي حساب
        /// </summary>
        /// <param name="_pageNo">
        /// شماره صفحه 
        /// </param>
        /// <param name="_seedNumber">
        /// تعداد ركوردهاي مدنظر براي مشاهده در خروجي
        /// </param>
        /// <returns></returns>
        public Models.SelectResultModelBinding<Models.Group> GetGroupAccounts(short _pageNo = 0,short _seedNumber = 10,string search = null)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_getAccountGroupList",true))
                {
                    pageNo = _pageNo;
                    seedNumber = _seedNumber;
                    repo.cmd.Parameters.AddWithValue("@search", search);
                    repo.ExecuteAdapter();
                    var info = repo.ds.Tables[0].AsEnumerable();
                    return new Models.SelectResultModelBinding<Models.Group>
                    {
                        Body = info.Select(i => new Models.Group
                        {
                            Id = Convert.ToInt32(i.Field<object>("id")),
                            Code = Convert.ToString(i.Field<object>("code")),
                            Title = Convert.ToString(i.Field<object>("title")),
                            IsDefault = Convert.ToBoolean(i.Field<object>("is_default")),
                            NatureId = Convert.ToInt16(i.Field<object>("fk_nature_id")),
                            NatureTitle = Convert.ToString(i.Field<object>("natureTitle"))
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
        /// <summary>
        /// ويرايش گروه حساب
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding UpdateGroup(Models.Group groupModel)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_updateAccountingGroup"))
                {
                    repo.cmd.Parameters.AddWithValue("@id", groupModel.Id);
                    repo.cmd.Parameters.AddWithValue("@code", groupModel.Code);
                    repo.cmd.Parameters.AddWithValue("@title", groupModel.Title);
                    repo.cmd.Parameters.AddWithValue("@natureId", groupModel.NatureId);
                    repo.cmd.Parameters.AddWithValue("@isDefault", groupModel.IsDefault);
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
        /// حذف يك گروه حساب
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding RemoveGroup(Models.Group groupModel,bool confirmDeleteSubdivision)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_deleteAccountGroup"))
                {
                    repo.cmd.Parameters.AddWithValue("@id", groupModel.Id);
                    repo.cmd.Parameters.AddWithValue("@confirmDeleteSubdivision", confirmDeleteSubdivision);
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
    }
}
