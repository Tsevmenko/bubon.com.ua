using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ViewModels.Views;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private int MAIN_LANGUAGE = 16;
        BLL.BLL bll;

        public HomeController()
        {
            bll = new BLL.BLL();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Catalog()
        {
            return View();
        }

        public ActionResult CatalogSection(int? id, int? delete)
        {
            if (delete.HasValue)
            {
                if (bll.DelProduct(delete.Value))
                {
                    System.IO.Directory.Delete(Server.MapPath("/images/products/" + delete.Value), true);
                }
            }
            CategoryModel model = new CategoryModel();
            var category = bll.GetCategories(MAIN_LANGUAGE).FirstOrDefault(p => p.id == id);
            if (id.HasValue && category != null)
            {
                model.id = category.id;
                model.parentId = (category.parentCategory.HasValue) ? category.parentCategory.Value : (int?)null;
                model.name = category.name;
                model.categories = bll.GetCategories(MAIN_LANGUAGE).Where(p => p.parentCategory == id).ToList();
                model.product = bll.GetProducts(id.Value);
                foreach (var el in model.product)
                {
                    var file = new System.IO.FileInfo(Server.MapPath(el.mainImage));
                    if (file.Exists)
                    {
                        el.mainImage = el.mainImage.Substring(1);
                    }
                    else
                    {
                        el.mainImage = null;
                    }
                }
            }
            else
            {
                model.name = "Категории";
                model.categories = bll.GetCategories(MAIN_LANGUAGE).ToList();
                model.product = bll.GetProducts();
                foreach (var el in model.product)
                {
                    var file = new System.IO.FileInfo(Server.MapPath(el.mainImage));
                    if (file.Exists)
                    {
                        el.mainImage = el.mainImage.Substring(1);
                    }
                    else
                    {
                        el.mainImage = null;
                    }
                }
            }
            return View(model);
        }

        public ActionResult ProductDetail(int id, bool? saveResult)
        {
            ExistingProduct entity = new ExistingProduct();
            entity.languages = from o in bll.GetLanguages()
                               select new SelectListItem
                               {
                                   Selected = false,
                                   Text = o.name,
                                   Value = o.id.ToString()
                               };
            entity.currencies = from o in bll.GetCurrency()
                                select new SelectListItem
                                {
                                    Selected = false,
                                    Text = o.name,
                                    Value = o.id.ToString()
                                };
            entity.categories = from o in bll.GetCategories(MAIN_LANGUAGE)
                                select new SelectListItem
                                {
                                    Selected = false,
                                    Text = o.name,
                                    Value = o.id.ToString()
                                };
            var product = bll.GetProducts().FirstOrDefault(p => p.id == id);
            if (product != null)
            {
                entity.id = product.id;
                entity.description = product.description;
                entity.name = product.name;
                entity.oldPrice = product.oldPrice;
                entity.price = product.price;
            }
            else
            {
                RedirectToAction("Products");
            }
            return View(entity);
        }
        [HttpPost]
        public JsonResult AddProductToCart(int id)
        {
            var entity = bll.GetProducts().FirstOrDefault(p => p.id == id);
            if (entity != null)
            {
                if (Session["cart"] != null)
                {
                    List<double> cart = (List<double>)Session["cart"];
                    bool pr = true;
                    for (int i = 0; i < cart.Count; i++)
                    {
                        if ((int)cart[i] == id)
                        {
                            cart[i] += 0.001;
                            pr = false;
                        }
                    }
                    if (pr)
                    {
                        double idNew = id + 0.001;
                        cart.Add(idNew);
                    }
                    Session["cart"] = cart;
                }
                else
                {
                    List<double> cart = new List<double>();
                    double idNew = id + 0.001;
                    cart.Add(idNew);
                    Session["cart"] = cart;
                }
                return new JsonResult() { Data = "good" };
            }
            else
            {
                return new JsonResult() { Data = "bad" };
            }

        }

        public ActionResult Cart()
        {
            CartModel model = new CartModel();
            if (Session["cart"] != null)
            {
                List<double> cart = (List<double>)Session["cart"];
                for (int i = 0; i < cart.Count; i++)
                {
                    var entity = bll.GetProducts().FirstOrDefault(p => p.id == Convert.ToInt32(cart[i]));
                    if (entity != null)
                    {
                        var item = new CartEntity()
                        {
                            id = entity.id,
                            name = entity.name,
                            price = entity.price,
                            quantity = Convert.ToInt32((cart[i] - Convert.ToInt32(cart[i])) * 1000),
                            imagePath = "/images/products/" + entity.id + "/main/main_thumbnail.jpg"
                        };
                        model.list.Add(item);
                    }
                }
                Session["cart"] = cart;
            }
            else
            {
                model.message = "Ваша корзина, к сожалению, пуста.";
            }
            return View(model);
        }

        public ActionResult DelProductFromCart(int id)
        {
            if (Session["cart"] != null)
            {
                List<double> cart = (List<double>)Session["cart"];
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i] == id)
                        cart.RemoveAt(i);
                }
                Session["cart"] = cart;
            }
            return new JsonResult() { Data = "All right" };
        }
        [HttpPost]
        public ActionResult RecalculateCart(CartModel model)
        {
            Session["cart"] = null;
            if (model.list != null)
            {
                List<double> cart = new List<double>();
                foreach (var el in model.list)
                {
                    double val = el.id + ((double)el.quantity / 1000);
                    if (((double)el.quantity / 1000) <= 0) continue;
                    cart.Add(val);
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Cart");
        }
        [HttpGet]
        public ActionResult Checkout(CheckoutModel model, int? ii)
        {
            if (model == null) model = new CheckoutModel();
            model.list = new List<CartEntity>();
            if (Session["cart"] != null)
            {
                List<double> cart = (List<double>)Session["cart"];
                for (int i = 0; i < cart.Count; i++)
                {
                    var entity = bll.GetProducts().FirstOrDefault(p => p.id == Convert.ToInt32(cart[i]));
                    if (entity != null)
                    {
                        var item = new CartEntity()
                        {
                            id = entity.id,
                            name = entity.name,
                            price = entity.price,
                            quantity = Convert.ToInt32((cart[i] - Convert.ToInt32(cart[i])) * 1000),
                            imagePath = "/images/products/" + entity.id + "/main/main_thumbnail.jpg"
                        };
                        model.total += entity.price * Convert.ToInt32((cart[i] - Convert.ToInt32(cart[i])) * 1000);
                        model.list.Add(item);
                    }
                }
                Session["cart"] = cart;
            }
            else
            {
                model.message = "Список Ваших покупок, к сожалению пуст, для начала необходмо что то купить.\r\nесли у Вас по каким то причинам не получается сделать это,\r\nпожалуйста, свяжитесь с нами по телефонам, указанным в шапке сайта.";
            }
            return View(model);
        }

        public ActionResult CheckoutDone()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutModel model)
        {
            if(ModelState.IsValid)
            {
                string from = "anton.tsevmenko@gmail.com";
                string to = "alla-bluz@ukr.net";
                string subj = "Заказ";
                string body = "Поступил заказ с сайта bubon.com.ua\r\n";
                body += "От " + model.name + "\r\nСостав заказа: \r\n";
                string username = "anton.tsevmenko@gmail.com";
                string pwd = "Cbybq L;by";

                foreach(var el in model.list)
                {
                    body += el.name + " " + el.quantity + " шт.\r\n";
                }
                body += "На общую стоимость : " + model.total + "\r\n";
                body += "Оплата: " + model.payWay + "\r\n";
                body += "Доставка: " + model.delivery + "\r\n";
                body += "Детали доставки: " + model.deliveryDetail + "\r\n";
                body += "Комментарий покупателя: " + model.comment + "\r\n";
                body += "Контакты покупателя: " + "\r\n";
                body += "Email: " + model.email + "\r\n";
                body += "Телефон: " + model.phone + "\r\n";
                
                Session["cart"] = null;
                
                var msg = new MailMessage(from, to, subj, body);
                var smtpClient = new SmtpClient("smtp.gmail.com", 25);
                smtpClient.Credentials = new NetworkCredential(username, pwd);
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
                return RedirectToAction("CheckoutDone");
            }
            else
            {
                return RedirectToAction("Checkout", new { model = model });
            }
        }

    }
}
