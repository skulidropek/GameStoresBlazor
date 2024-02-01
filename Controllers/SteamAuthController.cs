using GameStoresBlazor.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;

namespace GameStoresBlazor.Controllers
{
    [AllowAnonymous]
    public class SteamAuthController : Controller
    {
        [Route("auth/steam")]
        public IActionResult AuthenticateWithSteam()
        {
            // Здесь вы инициируете аутентификацию через Steam
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "Steam");
        }

        [Route("signin-steam")]
        public async Task<IActionResult> SignInSteam()
        {
            //var authenticateResult = await HttpContext.AuthenticateAsync("Steam");

            //if (!authenticateResult.Succeeded)
            //{
            //    // Обработка случая, когда аутентификация не удалась
            //    return BadRequest("Ошибка при аутентификации через Steam");
            //}

            //// Извлекаем информацию о пользователе
            //var steamIdClaim = authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier);
            //if (steamIdClaim == null)
            //{
            //    return BadRequest("Не удалось получить идентификатор Steam");
            //}

            //var steamId = steamIdClaim.Value;
            //var user = await _userManager.FindByLoginAsync("Steam", steamId);

            //if (user == null)
            //{
            //    // Пользователь не найден, регистрация нового пользователя
            //    user = new SteamIdentityUser { UserName = "steam_" + steamId };
            //    var createResult = await _userManager.CreateAsync(user);
            //    if (!createResult.Succeeded)
            //    {
            //        return BadRequest("Не удалось создать пользователя");
            //    }

            //    var addLoginResult = await _userManager.AddLoginAsync(user, new UserLoginInfo("Steam", steamId, "Steam"));
            //    if (!addLoginResult.Succeeded)
            //    {
            //        return BadRequest("Не удалось добавить вход Steam");
            //    }
            //}

            //// Вход в систему
            //await _signInManager.SignInAsync(user, isPersistent: false);

            // Перенаправление на главную страницу или страницу профиля после успешной аутентификации
            return Redirect("/");
        }
    }
}
