using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserIdentity.Models;

namespace UserIdentity.Controllers
{
    public class InputController : ApiController
    {
        private readonly HttpClient _httpClient;

        public InputController()
        {
            _httpClient = new HttpClient();  // İkinci API'ye veri göndermek için kullanacağız
        }

        [HttpPost]
        public IHttpActionResult PostInput(InputModel input)
        {
            // 1. Inputları doğrulama
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Hatalı veri varsa hata mesajı döndür
            }

            // 2. Veriyi ikinci API'ye gönder
            var secondApiUrl = "https://localhost:44335/api/Calculation";  // İkinci API'nin adresi
            var response = _httpClient.PostAsJsonAsync(secondApiUrl, input).Result;  // Veri gönderimi

            if (response.IsSuccessStatusCode)
            {
                return Ok("Veri başarıyla ikinci API'ye iletildi.");
            }
            else
            {
                return StatusCode(response.StatusCode);  // Hata durumunda uygun HTTP statü kodu döndür
            }
        }
    }
}
