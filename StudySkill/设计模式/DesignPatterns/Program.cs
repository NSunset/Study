﻿using CreationDesign.单例模式;
using CreationDesign.原型模式;
using CreationDesign.工厂方法模式;
using CreationDesign.抽象工厂模式;
using CreationDesign.建造者模式;
using System;
using System.Threading.Tasks;
using StructureDesign.装饰器模式;
using StructureDesign.代理模式;
using StructureDesign.适配器模式;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 创建型设计模式:对象的创建
            //单例模式
            {
                //for (int i = 0; i < 10; i++)
                //{
                //    Task.Run(() =>
                //    {
                //        User user = User.GetInstance();
                //    });
                //}

                //for (int i = 0; i < 10; i++)
                //{
                //    Task.Run(() =>
                //    {
                //        UserOne user = UserOne.GetInstance();
                //    });
                //}

                //for (int i = 0; i < 10; i++)
                //{
                //    Task.Run(() =>
                //    {
                //        UserTwo user = UserTwo.GetInstance();
                //    });
                //}

                //Console.ReadLine();
            }


            //原型模式
            {
                //Person person = new Person
                //{
                //    Name = "张三",
                //    IdInfo = new IdInfo
                //    {
                //        Id = 100
                //    }
                //};

                //Person person1 = person.GetClone();
                //person1.Name = "李四";
                //person1.IdInfo.Id = 200;

                //Console.WriteLine($"person:name:{person.Name};IdInfo.Id:{person.IdInfo.Id}");
                //Console.WriteLine($"person1:name:{person1.Name};IdInfo.Id:{person1.IdInfo.Id}");

                //person.Name = "王五";
                //person.IdInfo.Id = 300;

                //Console.WriteLine($"person:name:{person.Name};IdInfo.Id:{person.IdInfo.Id}");
                //Console.WriteLine($"person1:name:{person1.Name};IdInfo.Id:{person1.IdInfo.Id}");
            }

            //建造者模式
            {
                //Director director = new Director();
                //var tesla = new TeslaBuilder();
                //director.Builder = tesla;
                //director.ComponentTesla();

                //var biYaDi = new BiYaDiBuilder();
                //director.Builder = biYaDi;
                //director.ComponentBiYaDi();

                //tesla.Builder().Run();

                //biYaDi.Builder().Run();


            }


            //抽象工厂模式
            {
                //GUIFactory gUIFactory = new WinFactory();
                //IButton button = gUIFactory.CreateButton();
                //ICheckBox checkBox = gUIFactory.CreateCheckBox();


                //GUIFactory gUIFactory1 = new MacFactory();
                //IButton button1 = gUIFactory1.CreateButton();
                //ICheckBox checkBox1 = gUIFactory1.CreateCheckBox();

                //button.Paint();
                //checkBox.Paint();

                //button1.Paint();
                //checkBox1.Paint();

            }

            //工厂方法模式
            {
                //Creator creator = new SteamshipCreator();
                //creator.Operating();
            }

            #endregion

            #region 行为型设计模式:对象和行为的分离

            {

            }

            #endregion

            #region 结构型设计模式:类和类之间的关系

            {
                #region 装饰器模式

                {
                    //INotice notice = new PhoneNotice(null);
                    //notice.SendMessage();

                    //var notice1 = new EmailNotice(notice);
                    //notice1.SendMessage();

                    //var notice2 = new QQNotice(notice1);
                    //notice2.SendMessage();


                }

                #endregion

                #region 桥接模式

                {

                }

                #endregion

                #region 适配器模式

                {
                    //ICar car = new WulingHongguang();
                    //car.Run(50, 50);

                    //ICar car1 = new Adapter(new RailService());
                    //car1.Run(60, 60);
                }

                #endregion

                #region 组合模式

                {

                }

                #endregion

                #region 享元模式

                {

                }

                #endregion

                #region 代理模式

                {
                    //ISubject subject = new HttpRequest();

                    //HttpRequestProxy proxy = new HttpRequestProxy(subject);
                    //proxy.Request();

                }

                #endregion

                #region 外观模式

                {

                }

                #endregion

            }

            #endregion
        }
    }
}
