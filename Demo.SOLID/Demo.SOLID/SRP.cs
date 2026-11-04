using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SOLID
{

    public record Product(int Id, string Name, decimal Price);
   public class ProductProcessScreenBefore
    {
        public void Register(Product prodduct)
        {
            Console.WriteLine($"DB'ye {prodduct.Id}  id'li ürün kaydedildi");
        }

        public void SendEmail()
        {
            Console.WriteLine("Yöneticiye mail atıldı");
        }

        public void ChangeBackColor()
        {
            Console.WriteLine("Arayüz renkleri değiştirildi");
        }

        public void Process(Product product)
        {
            Register(product);
            SendEmail();
        }
    }

    public class ProductRepository
    {
        public void Create(Product product)
        {
            Console.WriteLine("Ürün db'ye eklendi");
        }
    }

    public class EmailSender
    {
        public void Send(string to, string body)
        {
            Console.WriteLine("Eposta gönderildi");
        }
    }

    public class ProductProcessScreenAfter
    {
        public void CreateProduct(string name, decimal Price)
        {
            ProductRepository repository = new ProductRepository();
            EmailSender emailSender = new EmailSender();

            repository.Create(new Product(1, name, Price));
            emailSender.Send("admin", "Ürün eklendi");
        }

        public void ChangeColor()
        {

        }
    }


}
