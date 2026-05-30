// See https://aka.ms/new-console-template for more information

using DesignPatterns.FactoryMethod.Applied;
using DesignPatterns.FactoryMethod;
using DesignPatterns.Builder;
using DesignPatterns.Adapter;
using DesignPatterns.Adapter.Ejemplo;
using DesignPatterns.Decorator;
using DesignPatterns.Mediator.Applied;
using DesignPatterns.State.Applied;
using DesignPatterns.Strategy.Applied;


//////////////////////////////////
/// Ejemplo factory
/// Parte 1

//string type = "Email";

//INotification notification;

//if (type == "Email")
//{
//    notification = new EmailNotification();
//}
//else
//{
//    notification = new SMSNotification();

//}

////////////////////
/// Parte 2: Implemetando
//IBroker broker = new SMSNotificator();

//INotification notification = broker.createNotification();


//notification.Send($"Hola se envió el mensaje");

//IBroker broker2 = new EmailNotificator();

// notification = broker2.createNotification();


//notification.Send($"Hola se envió el mensaje");

//Console.WriteLine("Hello, World!");
/////////////////////////////////////////////////////////

/// Ejemplo Builder
/// Parte 1

//var report = new PDFReport(
//    title: "Anual Report",
//    content:"Content now",
//    author: "Jesus",
//    footer: "Final",
//    creationDate: DateTime.UtcNow
//    );

//Console.WriteLine( $"Report: {report.Title} del Autor: {report.Author}" );

/// Parte 2
///// 
//PDFReportBuilder builder = new PDFReportBuilder;
//Director director = new Director(builder);

//var reportMinimal = director.BuildMinimalReport();
//var fullReport = director.BuildFulllReport();

//Console.WriteLine(fullReport.ToString());

////////////////////////////////////////
/// Ejemplo Adapter
/// Parte 1
//Weather weather = new Weather(80);
////Adapter
//LatamWeather latamWeather = new LatamWeather(12);
//WeatherAdapter weatherAdapter = new WeatherAdapter(latamWeather);

//// Reporter
//WeatherReport weatherReport = new WeatherReport();

//Console.WriteLine(weatherReport.ReportWeather(weather));

//// Mencion al clima en latam

//Console.WriteLine(weatherReport.ReportWeather(weatherAdapter));

/// Ejemplo webhook Adapter
/// 
//string webhookData = " evento: item_updated";

//List<IWebhookAdapter> webhooks = new List<IWebhookAdapter>
//{
//    new CompanyAAdapter(new CompanyAWebhookService(), "ABC-TOKEN-123"),
//    new CompanyBAdapter(new CompanyBWebhookService(), "Bearer eyASNaay809abB"),
//    new CompanyCAdapter(new CompanyCWebhookService(), "lferrufino", "admin")
//};

//foreach (var webhook in webhooks)
//{
//    webhook.ReceiveNotification(webhookData);
//}    




///// Decorator
///// Ejemplo decorador html

//IMessage message = new GeneralMessage();
//message = new HTMLDecorator(message);

//message = new FrameDecorator(message);

//message.Send("Usando decoradores");


//Console.ReadLine();



///// Mediator
///// Ejemplo mediator Dialog
//Chat chat = new Chat();
//var button = new ButtonMod(chat);
//var textbox = new TextBoxMod(chat);

//chat.Button = button;
//chat.TextBox = textbox;

//button.Click();

///// State
/// Ejemplo State
//DocumentContext context = new DocumentContext(new DraftState());
//context.Edit();
//context.Publish();
//context.Publish();
//context.Edit();

//// Strategy
/// Ejemplo Discount Strategy
/// 
DiscountContext calculator = new DiscountContext();

//Cliente premium
calculator.SetStrategy(new PremiumClientDiscount());
double price = 100;
double discountedPrice = calculator.CalculateDiscount(price);

Console.WriteLine($"Precio original: {price}, Precio con descuento para cliente premium: {discountedPrice}");

// Cliente regular
calculator.SetStrategy(new RegularClientDiscount());
discountedPrice = calculator.CalculateDiscount(price);
Console.WriteLine($"Precio original: {price}, Precio con descuento para cliente regular: {discountedPrice}");

// Cliente vip
calculator.SetStrategy(new VipClientDiscount());
discountedPrice = calculator.CalculateDiscount(price);
Console.WriteLine($"Precio original: {price}, Precio con descuento para cliente VIP: {discountedPrice}");