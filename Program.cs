
#region Create ingredients
using System;

Ingredient ingVodka = new Ingredient("Vodka", 15, 40);
Ingredient ingRum = new Ingredient("Rum", 15, 40);
Ingredient ingGin = new Ingredient("Gin", 15, 40);
Ingredient ingTripleSec = new Ingredient("Triple Sec", 20, 30);
Ingredient ingCola = new Ingredient("Cola", 1, 0);
Ingredient ingLimeJuice = new Ingredient("Lime Juice", 2, 0);
Ingredient ingCranJuice = new Ingredient("Cranberry Juice", 2, 0);
Ingredient ingGingerBeer = new Ingredient("Ginger Beer", 2, 4);
Ingredient ingMinWater = new Ingredient("Mineral Water", 1, 0);

List<Ingredient> ingredients = new List<Ingredient>
            {
                ingVodka,
                ingRum,
                ingGin,
                ingTripleSec,
                ingCola,
                ingLimeJuice,
                ingCranJuice,
                ingGingerBeer,
                ingMinWater
            };
#endregion

#region Create cocktails
Cocktail c1 = new Cocktail("Long Island Ice Tea");
c1.AddIngredient("Rum", 3);
c1.AddIngredient("Vodka", 3);
c1.AddIngredient("Gin", 3);
c1.AddIngredient("Cola", 9);

Cocktail c2 = new Cocktail("Moscow Mule");
c2.AddIngredient("Vodka", 4);
c2.AddIngredient("Lime Juice", 3);
c2.AddIngredient("Ginger Beer", 10);

Cocktail c3 = new Cocktail("Cosmopolitan");
c3.AddIngredient("Vodka", 4);
c3.AddIngredient("Triple Sec", 2);
c3.AddIngredient("Lime Juice", 6);
c3.AddIngredient("Cranberry Juice", 6);

Cocktail c4 = new Cocktail("Mojito");
c4.AddIngredient("Rum", 4);
c4.AddIngredient("Mineral Water", 10);
c4.AddIngredient("Lime Juice", 2);

List<Cocktail> cocktails = new List<Cocktail> { c1, c2, c3, c4 };
#endregion

//1.    The names of all cocktails.
var query1 = from c in cocktails
             select c.Name;
foreach (var name in query1) {
    Console.WriteLine(name);
}
Console.WriteLine("\n");

//2.	For each cocktail: The name of the cocktail, and the name and amount of all ingredients
var query2 = from c in cocktails
             select new { c.Name, c.Ingredients };
foreach (var item in query2) {
    Console.WriteLine($"\nCocktail name: {item.Name}");
    foreach (var ingre in item.Ingredients) {
        Console.WriteLine($"Ingredient name: { ingre.Key}, Amount: {ingre.Value}");
    }
}
Console.WriteLine("\n");

//3.	For each cocktail: The name of the cocktail, and the name of all ingredi-ents with an alcohol percentage above 10 %
var query3 = from c in cocktails
             select new 
             { c.Name, alcoIng = from cIng in c.Ingredients 
                                 join ing in ingredients 
                                 on cIng.Key equals ing.Name 
                                 where ing.AlcoholPercent > 10 
                                 select ing.Name };
foreach (var item in query3) {
    Console.WriteLine($"\nCocktail name: {item.Name}");
    foreach (var ing in item.alcoIng) {
        Console.WriteLine($"Ingredient name: {ing}");
    }
}
Console.WriteLine("\n");

//4.	For each cocktail: The name and the price of the cocktail
//(note that the price (per cl.) for an ingredient can be found in the Ingredient object collection).
var query4 = from c in cocktails
             select new {c.Name, cPrice = (from cCost in c.Ingredients
                                          join cost in ingredients
                                          on cCost.Key equals cost.Name
                                          select cCost.Value * cost.PricePerCl).Sum() };
foreach (var item in query4) {
    Console.WriteLine($"\nCocktail name: {item.Name}, Price: {item.cPrice}");
}
Console.WriteLine("\n");

//5.    For each cocktail: The name and the alcohol percentage of the cocktail.
var query5 = from c in cocktails
             select new
             {
                 c.Name,
                 Percentage = (from cAlcoPer in c.Ingredients
                               join alco in ingredients
                               on cAlcoPer.Key equals alco.Name
                               select cAlcoPer.Value * alco.AlcoholPercent).Sum() / 
                               (from cAlcoPer in c.Ingredients
                                join alco in ingredients
                                on cAlcoPer.Key equals alco.Name
                                select cAlcoPer.Value).Sum()
             };
foreach (var item in query5) {
    Console.WriteLine($"\nCocktail name: {item.Name}, Alcohol percentage: {item.Percentage}");
}