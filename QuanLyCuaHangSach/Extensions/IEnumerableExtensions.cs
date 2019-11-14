using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyCuaHangSach.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListTheLoai<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("TenTheLoai"),
                       Value = item.GetPropertyValue("IDTheLoai"),
                       Selected = item.GetPropertyValue("IDTheLoai").Equals(selectedValue.ToString())
                   };
        }
        public static IEnumerable<SelectListItem> ToSelectListTacGia<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("TenTacGia"),
                       Value = item.GetPropertyValue("IDTacGia"),
                       Selected = item.GetPropertyValue("IDTacGia").Equals(selectedValue.ToString())
                   };
        }
        public static IEnumerable<SelectListItem> ToSelectListNhaXuatBan<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("TenNhaXuatBan"),
                       Value = item.GetPropertyValue("IDNhaXuatBan"),
                       Selected = item.GetPropertyValue("IDNhaXuatBan").Equals(selectedValue.ToString())
                   };
        }

        public static IEnumerable<SelectListItem> ToSelectListItemString<T>(this IEnumerable<T> items, string selectedValue)
        {
            if(selectedValue==null)
            {
                selectedValue = "";
            }
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("Name"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedValue.ToString())
                   };
        }
    }
}
