﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;
using Newtonsoft.Json;
using System.IO;

namespace TodoApp.Services
{
    class FileIOService
    {
        private readonly string PATH;
        private object todoDataList;

        public FileIOService()
        {
        }

        public FileIOService(string path)
        {
            PATH = path;
        }

        public BindingList<TodoModel> LoadData()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<TodoModel>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
            }
        }

        public void SaveData(BindingList<TodoModel> _todoDataList)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(todoDataList);
                writer.Write(output);
            }
        }
    }
}
