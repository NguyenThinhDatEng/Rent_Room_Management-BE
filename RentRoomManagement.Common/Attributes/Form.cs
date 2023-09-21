﻿using RentRoomManagement.Common.Functions;
using RentRoomManagement.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentRoomManagement.Common.Attributes
{
    public class FormAttribute : ValidationAttribute
    {
        #region Field

        private string field;
        #endregion

        #region Constructor

        public FormAttribute(string field)
        {
            this.field = field;
        }
        #endregion

        #region Method

        /// <summary>
        /// Kiểm tra mã tài sản có nhập đúng định dạng không
        /// </summary>
        /// <param name="value">mã tài sản truyền vào</param>
        /// <returns>true nếu mã tài sản đúng định dạng</returns>
        /// <author>NVThinh 27/11/2022</author>
        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                bool isCode;
                // Nếu trường là mã tài sản
                if (field == Fields.fixed_asset_code)
                {
                    isCode = ValidateData.IsFixedAssetCode(value.ToString());
                    if (!isCode)
                    {
                        ErrorMessage = Errors.UserMsg_Fixed_Asset_Code;
                        return false;
                    }
                }
                // Nếu trường là mã bộ phận sử dụng
                if (field == Fields.department_code)
                {
                    isCode = ValidateData.IsDepartmentCode(value.ToString());
                    if (!isCode)
                    {
                        ErrorMessage = Errors.UserMsg_Department_Code;
                        return false;
                    }
                }
                // Nếu trường là mã loại tài sản
                if (field == Fields.fixed_asset_category_code)
                {
                    isCode = ValidateData.IsFixedAssetCategoryCode(value.ToString());
                    if (!isCode)
                    {
                        ErrorMessage = Errors.UserMsg_Fixed_Asset_Category_Code;
                        return false;
                    }
                }
                return true;
            }

            return true;
        }
        #endregion
    }
}
