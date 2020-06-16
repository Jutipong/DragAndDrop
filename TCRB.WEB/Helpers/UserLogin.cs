using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Web;
using TCRB.DAL.Model.Authentication;
using TCRB.DAL.Model.UserLogin;

namespace TCRB.WEB
{
    public class UserLogin
    {
        public UserProfileModel UserProfile()
        {
            var userCurrent = HttpContext.Current.User;
            var userJson = userCurrent.Claims.Where(u => u.Type == ClaimTypes.Sid).Select(r => r.Value).FirstOrDefault();
            var userdatajson = userCurrent.Claims.Where(u => u.Type == ClaimTypes.UserData).Select(r => r.Value).FirstOrDefault();
            var user = JsonSerializer.Deserialize<UserProfileModel>(userdatajson);
            return user;
        }
    }
}
