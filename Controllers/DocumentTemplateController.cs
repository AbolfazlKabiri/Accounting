using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
    class DocumentTemplateController:Repository.Repository
    {
        /// <summary>
        /// ثبت الگوی سند جدید
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding InsertDocumentTemplate(Models.DocumnetTemplateModelBinding model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_insertAccountingDocumentTemplate"))
                {
                    
                    repo.cmd.Parameters.AddWithValue("@title", model.Title);
                    repo.cmd.Parameters.AddWithValue("@descriptionHead", model.DescriptionHeader);
                    repo.cmd.Parameters.AddWithValue("@documentTypeId", model.DocumentTypeId);
                    repo.cmd.Parameters.AddWithValue("@accountCode", model.TafsilCode);
                    repo.cmd.Parameters.AddWithValue("@natureId", model.NatureId);
                    repo.cmd.Parameters.AddWithValue("@descriptionRow", model.DescriptionRow);
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
        /// ویرایش الگوی سند
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding UpdatetDocumentTemplate(Models.DocumnetTemplateModelBinding model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_updateAccountingDocumentTemplate"))
                {
                    repo.cmd.Parameters.AddWithValue("@id", model.Id);
                    repo.cmd.Parameters.AddWithValue("@title", model.Title);
                    repo.cmd.Parameters.AddWithValue("@descriptionHead", model.DescriptionHeader);
                    repo.cmd.Parameters.AddWithValue("@documentTypeId", model.DocumentTypeId);
                    repo.cmd.Parameters.AddWithValue("@accountCode", model.TafsilCode);
                    repo.cmd.Parameters.AddWithValue("@natureId", model.NatureId);
                    repo.cmd.Parameters.AddWithValue("@descriptionRow", model.DescriptionRow);
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
        /// حذف الگوی سند
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding DeletetDocumentTemplate(Models.DocumnetTemplateModelBinding model)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_deleteAccountingDocumentTemplate"))
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
        /// دريافت ليست الگوی اسناد
        /// </summary>
        /// <param name="_pageNo">
        /// شماره صفحه 
        /// </param>
        /// <param name="_seedNumber">
        /// تعداد ركوردهاي مدنظر براي مشاهده در خروجي
        /// </param>
        /// <returns></returns>
        public Models.SelectResultModelBinding<Models.DocumnetTemplateModelBinding> GettDocumentTemplates(int docTypeId,short _pageNo = 0,short _seedNumber = 10,string search = null)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_getTafsilAccountList",true))
                {
                    pageNo = _pageNo;
                    seedNumber = _seedNumber;
                    
                    repo.cmd.Parameters.AddWithValue("@docTopId", docTypeId);
                    repo.ExecuteAdapter();
                    var info = repo.ds.Tables[0].AsEnumerable();
                    
                    return new Models.SelectResultModelBinding<Models.DocumnetTemplateModelBinding>
                    {
                        Body = info.Select(i => new Models.DocumnetTemplateModelBinding()
                        {
                            Id = Convert.ToInt32(i.Field<object>("id")),
                            Title = Convert.ToString(i.Field<object>("title")),
                            TafsilCode = Convert.ToString(i.Field<object>("fk_account_code")),
                            DescriptionHeader = i.Field<object>("description_head").ToString(),
                            DescriptionRow = Convert.ToString(i.Field<object>("description_row")),
                            DocumentTypeId = Convert.ToInt32(i.Field<object>("docTypeId")),
                            DocumentTypeTitle = Convert.ToString(i.Field<object>("docTypeTitle")),
                            NatureId = Convert.ToString(i.Field<object>("fk_nature_id")),
                            NatureTitle = i.Field<object>("natureTitle").ToString()
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
