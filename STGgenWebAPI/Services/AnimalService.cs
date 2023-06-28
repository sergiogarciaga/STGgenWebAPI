using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using STGgenWebAPI.Data;
using STGgenWebAPI.Model;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace STGgenWebAPI.Services
{
    public class AnimalService : IAnimalService
    {
        readonly STGeneticsDBContext context;
        public AnimalService()
        {
            context = new STGeneticsDBContext();
        }

        public async Task Delete(int id)
        {
            var animal = context.Animals.Find(id);

            if (animal != null)
            {
                context.Remove(animal);
                await context.SaveChangesAsync();
            }

        }

        public async Task Update(int id, Animal animal)
        {

            var animalActual = context.Animals.Find(id);

            if (animalActual != null)
            {
                animalActual.Price = animal.Price;
                animalActual.Status = animal.Status;
                animalActual.Name = animal.Name;
                animalActual.Sex = animal.Sex;
                animalActual.Breed = animal.Breed;
                animalActual.BirthDate = animal.BirthDate;

                context.Update(animalActual);
                await context.SaveChangesAsync();
            }
        }

        public IEnumerable<Animal> FilterAnimals(string sex, string status)
        {
            var query = context.Animals.AsQueryable();



            if (!string.IsNullOrEmpty(sex))
            {
                query = query.Where(a => a.Sex == sex);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(a => a.Status == status);
            }

            return query.ToList();

            // var animals = query.ToList();
            //    var animals = context.Animals.Select( a => a.Sex == sex || a.Status == status )  ; //query.ToList();
            //  return animals;
        }


        public IEnumerable<Animal> FilterAnimals(int animalId, string name, string sex, string status)
        {

            return context.Animals.FromSqlInterpolated($"EXECUTE FilterAnimals @AnimalId = {animalId}, @Name = {name}, @Sex = {sex}, @Status = {status}").ToList();


        }




        public async Task Save(Animal animal)
        {
            context.Add(animal);
            await context.SaveChangesAsync();
        }

        public int CreateOrder(List<Animal> AnimalsOrder)
        {
            var order = new Order
            {
                TotalQuantity = AnimalsOrder.Count,
                TotalPurchaseAmount = AnimalsOrder.Sum(animal => animal.Price)
            };



            //            o Business Rules:
            //▪	If the customer adds an animal with a quantity greater than 50 in the cart, we must apply a 5 % discount on the value of this animal.
            if (order.TotalQuantity > 50)
            {
                order.TotalPurchaseAmount *= (decimal)0.95;
            }
            //▪	If the customer buys more than 200 animals in the order, an additional 3 % discount will be added to the total purchase price.

            if (order.TotalQuantity > 200)
            {
                order.TotalPurchaseAmount *= (decimal)0.93;
            }
            //▪	If the customer buys more than 300 animals in the order, the freight value must be free, otherwise it will charge $1,000.00 for freight.

            if (order.TotalQuantity <= 300)
            {
                order.TotalPurchaseAmount += 1000;
            }

            context.Orders.Add(order);
            


            context.SaveChanges();

            var orderId = order.OrderId;

            foreach (var animal in AnimalsOrder)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    AnimalId = animal.AnimalId
                };
                context.OrderDetails.Add(orderDetail);
            }
            context.SaveChanges();
            return orderId;
        }

        public bool ValidateOrder(List<Animal> animalsOrder)
        {
            //▪	It is not allowed to duplicate the animal in the Order.If you identify the duplicate animal, the API should return an error message displaying the reason.

            if (animalsOrder
        .GroupBy(animal => animal.AnimalId)
        .Any(group => group.Count() > 1))
            {
                return false;

            }

            return true;
        }
    }

    public interface IAnimalService
    {
        IEnumerable<Animal> FilterAnimals(string sex, string status);

        IEnumerable<Animal> FilterAnimals(int animalId, string name,    string sex, string status);
        Task Update(int id, Animal animal);

        Task Delete(int id);
        Task Save(Animal animal);
        int CreateOrder(List<Animal> order);
        bool ValidateOrder(List<Animal> order);
    }
}
