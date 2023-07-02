using Microsoft.Extensions.Logging;

namespace sunstealer.mvc.Controllers
{
    [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(IgnoreApi = true)]
    public class Generic : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly Microsoft.Extensions.Logging.ILogger<Generic> _logger;
        private readonly static System.Collections.Generic.Dictionary<System.Guid, Models.Generic> data = new();

        // ajm: data
        private static System.Collections.Generic.Dictionary<System.Guid, Models.Generic> Data {
            get {
                for (int i = 0; i < 10; i++) {
                    Models.Generic m = new() {
                        Id = System.Guid.NewGuid(),
                        Integer = i,
                        String = $"string{i}",
                    };
                    Generic.data.Add(m.Id, m);
                }
                return Generic.data;
            }
        }

        private readonly Services.IGeneric genericService;
        private readonly System.Net.Http.HttpClient hhtpClient;

        public Generic(
            Services.IGeneric genericService,
            System.Net.Http.HttpClient hhtpClient, 
            Microsoft.Extensions.Logging.ILogger<Generic> logger)
        {
            this.genericService = genericService;
            this.hhtpClient = hhtpClient;
            _logger = logger;
            _logger.LogInformation("Generic.Generic()");
            this.genericService.ConsoleWriteMessage("Generic.Generic()");
        }

        // ajm: create ----------------------------------------------------------------------------
        // ajm: GET: Generic/Create
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("~/Generic/Create")]
        public Microsoft.AspNetCore.Mvc.ActionResult Create()
        {
            _logger.LogInformation("Microsoft.AspNetCore.Mvc.ActionResult Create()");
            return View();
        }

        // ajm: POST: Generic/Create
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("~/Generic/Create")]
        public Microsoft.AspNetCore.Mvc.ActionResult Create(Microsoft.AspNetCore.Http.IFormCollection collection)
        {
            try
            {
                Models.Generic m = new() {
                    Id = System.Guid.NewGuid(),
                    Integer = int.Parse(collection["Integer"][0]),
                    String = collection["String"][0],
                };
                Generic.Data.Add(m.Id, m);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // ajm: delete ----------------------------------------------------------------------------
        // ajm: GET: Generic/Delete/id
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("~/Generic/Delete/{id?}")]
        public Microsoft.AspNetCore.Mvc.ActionResult Delete(System.Guid id)
        {
            _logger.LogInformation($"delete prompt id: {id}");

            if (data.TryGetValue(id, out Models.Generic? model))
            {
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        // ajm: POST: Generic/Delete/id
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("~/Generic/Delete/{id?}")]
        public Microsoft.AspNetCore.Mvc.ActionResult Delete(System.Guid id, Microsoft.AspNetCore.Http.IFormCollection collection)
        {
            try
            {
                _logger.LogInformation($"delete commit id: {id}");
                Generic.Data.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
 


        // ajm: read ------------------------------------------------------------------------------
        // ajm: GET: Generic => Index
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("~/Generic")]
        public Microsoft.AspNetCore.Mvc.ActionResult Index()
        {
            return View(new System.Collections.Generic.List<Models.Generic>(Generic.Data.Values));
        }

        // ajm: GET: Generic/Read/id = > Read
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("~/Generic/Read/{id?}")]
        public Microsoft.AspNetCore.Mvc.ActionResult Read(System.Guid id)
        {
            _logger.LogInformation($"Read id: {id} ");
            if (data.TryGetValue(id, out Models.Generic? model))
            {
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }



        // ajm: update ----------------------------------------------------------------------------
        // ajm: GET: Generic/Update/id
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("~/Generic/Update/{id?}")]
        public Microsoft.AspNetCore.Mvc.ActionResult Update(System.Guid id)
        {
            _logger.LogInformation($"update prompt id: {id} ");
            if (data.TryGetValue(id, out Models.Generic? model))
            {
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        // ajm: POST: Generic/Update/id
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("~/Generic/Update/{id?}")]
        public Microsoft.AspNetCore.Mvc.ActionResult Update(System.Guid id, Microsoft.AspNetCore.Http.IFormCollection collection)
        {
            try
            {
                _logger.LogInformation($"update commit id: {id} ");
                Models.Generic m = new() {
                    Integer = int.Parse(collection["Integer"][0]),
                    String = collection["String"][0],
                };
                Generic.Data[id] = m; 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
   }
}