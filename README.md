# gemi-kayit-yonetim
Gemi kayıt ve kaydedici yönetim uygulaması.

Ana özellikler:

1-Admin, kayıt ekleme yetkisi olan kullanıcılar ekleyebilir, düzenleyebilir ve kaldırabilir.
2-Admin, gemi adı ekleyebilir ve kaldırabilir.
3-Yükleme yetkisi olan kullanıcılıar, bir gemi olayı ekleyecek.Olayın referans numarası, tekil(unique)dir.
4-Yükleme ekranında zorunlu olarak olayın referans numarası girilecek, gemi adı seçilecek,
opsiyonel olarak açıklama girilecek, zorunlu olarak olayla ilgili bir veya birkaç fotoğraf eklenecek ve
olayın tarihi seçilecek ve bu şekilde veritabanına girilecek.
5-Resimler server tarafında küçültülecek, küçültülmüş hali veritabanında, orijinal hali ise bir klasörde saklanacak.
6-Resmin orijinalinin linki veritabanında tutulacak.
7-Kullanıcı, dilediğinde kendi girdiği kayıtları görebilecek ve üzerinde değiştirme yapabilecek.
8-Değiştirme yaparken yeni fotolar ekleyebilecek, veya olanları silebilecek.
9-Admin, bir gemi adı sildiğinde, o gemi adına sahip olan tüm kayıtlar da silinecek.Bunun için öncelikle tüm kayıtların
silineceği ekranda bilgi olarak verilecek, ve devam etmesi için onaylaması istenecek.
10-Arama ekranı olacak.Arama 4 şekilde yapılabilecek:Referans no ile, gemi adı ile, tarih aralığı ile, 
hem gemi adı hem de tarih aralığı ile.
11-Gemi adı ve tarih aralığı ile yapılan aramalarda birden fazla sonuç geri dönebilecek, kullanıcı bunların linklerine
tıklayarak detaylarına ulaşabilecek.
Projeye başlama tarihi:7 Temmuz 2015.
Github'a eklenme tarihi:16 Temmuz 2015.
