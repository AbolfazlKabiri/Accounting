﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Controllers
{
   public class CostCenterController:Repository.Repository
    {
         /// <summary>
        /// ثبت مرکز هزینه جديد
        /// </summary>
        /// <param name="groupModel">
        /// مدل پايه مرکز هزینه
        /// </param>
        /// <returns></returns>
        public Models.ActionResultModelBinding InsertCostCenter(Models.CostCenter groupModel)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_insertCostCenter"))
                {
                    repo.cmd.Parameters.AddWithValue("@code", groupModel.Code);
                    repo.cmd.Parameters.AddWithValue("@title", groupModel.Title);
        
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
        /// دريافت ليست مراکز هزینه
        /// </summary>
        /// <param name="_pageNo">
        /// شماره صفحه 
        /// </param>
        /// <param name="_seedNumber">
        /// تعداد ركوردهاي مدنظر براي مشاهده در خروجي
        /// </param>
        /// <returns></returns>
        public Models.SelectResultModelBinding<Models.PeopleGroup> GetCostCenter(short _pageNo = 0,short _seedNumber = 10,string search = null)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_getCostCenterList",true))
                {
                    pageNo = _pageNo;
                    seedNumber = _seedNumber;
                    repo.cmd.Parameters.AddWithValue("@search", search);
                    repo.ExecuteAdapter();
                    var info = repo.ds.Tables[0].AsEnumerable();
                    return new Models.SelectResultModelBinding<Models.PeopleGroup>
                    {
                        Body = info.Select(i => new Models.PeopleGroup
                        {
                            Id = Convert.ToInt32(i.Field<object>("id")),
                            Code = Convert.ToString(i.Field<object>("code")),
                            Title = Convert.ToString(i.Field<object>("title")),
                        
                            SaveDateTime = Convert.ToDateTime(i.Field<object>("save_datetime"))
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
        /// ويرايش مرکز هزینه
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding UpdateCostCenter(Models.CostCenter groupModel)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_updatetCostCenter"))
                {
                    repo.cmd.Parameters.AddWithValue("@id", groupModel.Id);
                    repo.cmd.Parameters.AddWithValue("@code", groupModel.Code);
                    repo.cmd.Parameters.AddWithValue("@title", groupModel.Title);
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
        /// حذف مرکز هزینه
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        public Models.ActionResultModelBinding RemoveCostCenter(Models.CostCenter groupModel,bool confirmDeleteSubdivision)
        {
            try
            {
                using (var repo = new Repository.Repository(this, "usp_deleteCostCenter"))
                {
                    repo.cmd.Parameters.AddWithValue("@id", groupModel.Id);
                    //repo.cmd.Parameters.AddWithValue("@confirmDeleteSubdivision", confirmDeleteSubdivision);
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
