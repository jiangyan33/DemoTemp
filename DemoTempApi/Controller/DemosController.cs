using DemoTempApi.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoTempApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemosController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Person>> Get([FromQuery] Person person)
        {
            var result = await LoadData();

            if (person == null) return result;

            return result.Where(x =>
             {
                 var flag = false;

                 flag = person.Id == 0 || x.Id == person.Id;

                 if (flag)
                     flag = string.IsNullOrEmpty(person.Name) || x.Name.Contains(person.Name);

                 return flag;
             }).ToList();
        }

        public async Task<List<Person>> LoadData()
        {
            var result = new List<Person>();
            var random = new Random();
            var names = new List<string> { "张三", "李四", "王二", "麻子" };

            // 模拟异步方法
            await Task.Run(() =>
             {
                 System.Threading.Thread.Sleep(500);
                 for (var i = 0; i < 10; i++)
                 {
                     // .net 5.0新语法，挺好玩的
                     result.Add(new()
                     {
                         Id = i + 1,
                         Name = names[random.Next(0, 4)]
                     });
                 }
             });

            return result;
        }
    }
}