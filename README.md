# RAISERROR ile kullanıcı taraflı mesajların fırlatılması C# SqlException içinde ayırt edilmesi


https://medium.com/@kayaism/raiserror-ile-kullan%C4%B1c%C4%B1-tarafl%C4%B1-mesajlar%C4%B1n-f%C4%B1rlat%C4%B1lmas%C4%B1-bafc4699ca10

Database programlama ile uğraşıyorsanız, iş ihtiyaçlarını yüklenen sql kodlarınız var demektir. Prosedür parametrelerine dayanan veya iş ihtiyaçlarına göre kullanıcı mesajı vererek, prosedürün sonlanmasına ihtiyaç duyabilirsiniz. Ancak bu mesajları kod seviyesinde yakaladığınızda da kullanıcı taraflı bir mesaj mı yoksa sistemsel bir hata mı oluştuğu bilmeniz gerekir. Böylelikle sistem mesajlarını loglayıp ilgili developer veya dba tarafından incelenmesini sağlayabilirsiniz. Ayrıca kullanıcı için oluşturulmuş bir mesajı da kullanıcıya gösterebilirsiniz.
Biz ekibimizde bunun için olarak sp_mkmError isminde kendi geliştirdiğimiz bir prosedürü kullanıyoruz. Bu prosedürün işi kullanıcı mesajlarını tuttuğumuz tablodan uygun olan mesajı alarak RAISERROR ile hata fırlatmak. Şimdi isterseniz RAISEERROR’ün oluşan hataları nasıl fırlattığına bakalım.

SQL Server 0 bölünme hatası fırlatır.
Sql Server sistemsel oluşan mesajlarını sys.messages tablosundan Error Number, Error Message bilgilerini Language ve Level bilgisiyle birlikte fırlatıyor.
Msg 8134, Level 16, State 1, Line 8
Divide by zero error encountered.
sys.messages tablosunda message_id ve language_id key alanları
RAISERROR SQL Server ‘ın hata fırlatma yöntemlerinden birisidir. RAISERROR sessionda oluşan sistemsel veya kullanıcı tanımlı hataları set etmek için kullanılır. sp_addmessage prosedürünü kullanarak error number 50000 den büyük hata mesajlarını kaydetmemize ve sonra fırlatmanız içinde uygundur. Ayrıca bizim yaptığımız gibi kendi mesaj tablonuzdan, ilgili mesajı argüman olarak ta kullanabilirsiniz. Bu RAISERROR oluşturma parametrelerinin, hem error number hemde mesaj stringi alabilmesi sayesinde olur. Böyle bir durumda error number 50000 sabit olarak atanır. Böylelikle Error number sistemsel hatalar ile kullanıcı taraflı oluşturulan hatalar için ayırt edici olur.
— Syntax for SQL Server and Azure SQL Database 

RAISERROR ( { msg_id | msg_str | @local_variable } 
{ ,severity ,state } 
[ ,argument [ ,…n ] ] ) 
[ WITH option [ ,…n ] ]
When msg_str is specified, RAISERROR raises an error message with an error number of 50000.
Biz projemizde Ado.net kullanarak sorgularımızı çalıştırıyoruz. Burada yakaladığımız exception tipi SqlException ise içerisinde Error bilgisine erişebiliyoruz. Buradaki Error Number bilgisi 50000 ise bunun kendi fırlattığımız mesajlar olduğunu anlıyoruz.
