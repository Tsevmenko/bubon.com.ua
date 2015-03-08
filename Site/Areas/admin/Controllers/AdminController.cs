using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using ViewModels.Views;

namespace Site.Areas.admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /admin/Admin/

        private int MAIN_LANGUAGE = 16; // main admin language

        BLL.BLL bll;

        public AdminController()
        {
            bll = new BLL.BLL();
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Currencies(bool res = false)
        {
            Currency model = new Currency()
            {
                currencies = bll.GetCurrency(),
                newCurrency = new ViewModels.CurrencyVM(),
                res = res
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Currencies(Currency model)
        {
            bool result = false;
            if (model.currencies != null)
            {
                foreach (var el in model.currencies)
                {
                    if (el.del)
                    {
                        bll.DelCurrency(el.id);
                    }
                    else
                    {
                        bll.UpdateCurrency(el.id, el.name);
                    }
                }
                result = true;
            }
            if (model.newCurrency != null && model.newCurrency.name != null && model.newCurrency.name.Length > 0)
            {
                if (bll.AddCurrency(model.newCurrency.name) > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return RedirectToAction("Currencies", new { res = result });
        }
        public ActionResult Languages(bool res = false)
        {
            Language model = new Language()
            {
                languages = bll.GetLanguages(),
                newLanguage = new ViewModels.LanguageVM(),
                res = res
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Languages(Language model)
        {
            bool result = false;
            if (model.languages != null)
            {
                foreach (var el in model.languages)
                {
                    if (el.del)
                    {
                        bll.DelLanguage(el.id);
                    }
                    else
                    {
                        bll.UpdateLanguage(el.id, el.name, el.isDefault);
                    }
                }
                result = true;
            }
            if (model.newLanguage != null && model.newLanguage.name != null && model.newLanguage.name.Length > 0)
            {
                if (bll.AddLanguage(model.newLanguage.name, model.newLanguage.isDefault) > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return RedirectToAction("Languages", new { res = result });
        }

        public ActionResult Categories(bool res = false)
        {
            Categories model = new Categories();
            model.languages = new Dictionary<int, string>();
            model.newCategory = new NewCategory();
            foreach (var el in bll.GetLanguages())
            {
                List<CategoryVM> categories = bll.GetCategories(el.id);

                model.categories.Add(new CategoriesInsider()
                {
                    languageId = el.id,
                    categories = categories
                });
                model.languages.Add(el.id, el.name);
                model.newCategory.names.Add(new CategoryName() { languageId = el.id, name = "" });
            }
            model.newCategory.ParentCategory =
            from c in bll.GetCategories(MAIN_LANGUAGE)
            select new SelectListItem
            {
                Selected = false,
                Text = c.name,
                Value = c.id.ToString()
            };

            model.res = res;
            return View(model);
        }
        public ActionResult Category(int? id, int? delete)
        {
            if (delete.HasValue)
            {
                if (bll.DelProduct(delete.Value))
                {
                    if (System.IO.Directory.Exists(Server.MapPath("/images/products/" + delete.Value)))
                    {
                        System.IO.Directory.Delete(Server.MapPath("/images/products/" + delete.Value), true);
                    }
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
        [HttpPost]
        public ActionResult Categories(Categories model)
        {
            bool result = false;
            if (model.categories != null)
            {
                foreach (var el in model.categories)
                {
                    foreach (var obj in el.categories)
                    {
                        if (obj.del)
                        {
                            bll.DelCategory(obj.id);
                        }
                        else
                        {
                            bll.UpdateCategory(obj.id, obj.name, obj.languageId, obj.parentCategory);
                        }
                    }
                }
                result = true;
            }
            if (model.newCategory != null && model.newCategory.names != null)
            {
                bool pr = false;
                foreach (var el in model.newCategory.names)
                    if (el.name != null) { pr = false; break; }
                    else pr = true;
                if (model.newCategory.names != null && model.newCategory.names.Count() > 0 && !pr)
                {
                    if (model.newCategory.parentCategoryId == 0) model.newCategory.parentCategoryId = null;
                    int id = bll.AddCategory(model.newCategory.names[0].name, model.newCategory.names[0].languageId, model.newCategory.parentCategoryId);
                    if (id > 0)
                        foreach (var el in model.newCategory.names)
                        {
                            if (el.name == null) el.name = "";
                            result = bll.UpdateCategory(id, el.name, el.languageId, model.newCategory.parentCategoryId);
                        }
                }
            }
            return RedirectToAction("Categories", new { res = result });
        }
        //Product
        public ActionResult AddProduct(NewProduct entity)
        {
            if (!entity.save || entity == null)
            {
                entity = new NewProduct();
            }
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
            return View(entity);
        }
        [HttpPost]
        public ActionResult AddProduct(NewProduct model, IEnumerable<HttpPostedFileBase> files, HttpPostedFileBase mainPhoto)
        {
            if (ModelState.IsValid)
            {
                int id = bll.AddProduct(model.name, (decimal)model.price, (decimal?)model.oldPrice, model.description, model.categoryIds, new List<SizeVM>(), new List<ColorVM>());
                if (id > 0)
                {
                    //save photos
                    string path = Server.MapPath(".\\").Replace("\\admin\\", "") + "\\images\\products\\";// +id;
                    System.IO.Directory.CreateDirectory(path);
                    //save main page
                    if (mainPhoto != null && mainPhoto.ContentLength > 0)
                    {
                        string mainPath = path + id + "/main/";
                        System.IO.Directory.CreateDirectory(mainPath);
                        mainPath += "main.jpg";
                        mainPhoto.SaveAs(mainPath);
                        var thumbnail = ScaleImage(Image.FromFile(mainPath), 265, 260);//(Image)(new Bitmap(, new System.Drawing.Size(265, 260)));
                        thumbnail.Save(mainPath.Replace(".jpg", "_thumbnail.jpg"));
                    }
                    //save other photos
                    if (files != null && files.Count() > 0)
                    {
                        foreach (var el in files)
                        {
                            if (el == null) continue;
                            string filePath = path + "\\" + id + "\\" + el.FileName;
                            el.SaveAs(filePath);
                        }
                    }
                    //end save photos
                }
                return RedirectToAction("ProductDetail", new { id = id, saveResult = true });
            }
            model.save = true;
            return AddProduct(model);

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
        public ActionResult Products(int? id)
        {
            List<ExistingProduct> products = new List<ExistingProduct>();
            if (id.HasValue)
            {
                products = bll.GetProducts(id.Value).Select(p => new ExistingProduct()
                {
                    id = p.id,
                    name = p.name,
                    price = p.price,
                    oldPrice = p.oldPrice,
                    description = p.description
                }).ToList();
            }
            else
            {
                products = bll.GetProducts().Select(p => new ExistingProduct()
                {
                    id = p.id,
                    name = p.name,
                    price = p.price,
                    oldPrice = p.oldPrice,
                    description = p.description
                }).ToList();
            }
            return View(products);
        }
        public ActionResult ProductRedact(int id)
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
        public ActionResult ProductRedact(ExistingProduct model)
        {
            if (bll.UpdateProduct(model.id, model.name, (decimal)model.price, (decimal)model.oldPrice, model.description, model.categoryIds, new List<SizeVM>(), new List<ColorVM>()))
            {
                model.save = true;
            }
            else
            {
                model.save = false;
            }
            return View(model);
        }
    }
}
