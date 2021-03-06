using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Constants
{
    public static class Messages
    {
        public static string Added = "Eklendi";
        public static string Deleted = "Silindi";
        public static string Uptaded = "Güncellendi";
        public static string NameAlreadyRegistered = "Girdiğiniz ad önceden eklenilmiştir.";
        public static string NotFound = "Bulunamadı.";
        public static string Found = "Bulundu.";
        public static string CountEqualsZero = "Eklenen veri/veriler bulunamadı. Lütfen eklemek için tıklayınız.";

        public static string YearAlreadyRegistered = "Girdiğiniz yıl önceden eklenilmiştir.";

        public static string CarNotReturn = "Kiralamak istediğiniz araö şuan kiradadaır.";

        public static string CarLimited = "Arabaya resim yükleme limiti aşıldı. Resim eklemek için önceki resimleri düzenleyin veya silin.";

        public static string UserRegistered = "Kayıt başarılı.";
        public static string PasswordError = "Şifre hatalı.";
        public static string LoginSuccessful = "Giriş başarılı.";
        public static string UserAlreadyExist = "Kullanıcı mevcut.";
        public static string CreateAccessToken = "Access Token oluşturuldu.";
        public static string AuthorizationDenied = "Yetkiniz yok.";

    }
}
