using Microsoft.Web.WebView2.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWpf.Invoke
{


    public class BlazorInterop
    {
        private CoreWebView2 WebView2 { get; }

        public BlazorInterop(CoreWebView2 webView2)
        {
            WebView2 = webView2;
        }

        public void OpenFile()
        {
            // 打开文件对话框，获取用户选择的文件路径
            string filePath = OpenFileUsingWpf();

            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            // 将文件转换为字节数组或流
            byte[] fileBytes = File.ReadAllBytes(filePath);

            // 将字节数组或流传递回 Blazor 页面
            WebView2.ExecuteScriptAsync($"receiveFile('{Convert.ToBase64String(fileBytes)}')");
        }

        private string OpenFileUsingWpf()
        {
            // 在 WPF 中实现打开文件对话框的逻辑


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel 文件 (*.xlsx; *.xls) | *.xlsx; *.xls";
            openFileDialog.Title = "选择 Excel 文件";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                // 在这里可以继续处理选中的 Excel 文件
                return selectedFilePath;
            }
            else
            {
                return string.Empty;
            }
        }
    }

}