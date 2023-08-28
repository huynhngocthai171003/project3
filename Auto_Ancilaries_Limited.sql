CREATE DATABASE [ePRJ]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ePRJ', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ePRJ.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ePRJ_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ePRJ_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

CREATE TABLE Customers(
    Id int IDENTITY(1,1) NOT NULL,
    UserName varchar(100) NOT NULL,
    FullName varchar(100) NOT NULL,
    Email varchar(200) NOT NULL,
    PhoneNumber varchar(13) NOT NULL,
    UserAddress varchar(100) NOT NULL,
    PassWord varchar(200) NOT NULL,
    Active int DEFAULT 1,
    PRIMARY KEY (Id)
)
CREATE TABLE Categorys(
    Id int IDENTITY(1,1) NOT NULL,
    Name varchar(50) NOT NULL,
    PRIMARY KEY (Id),
)

CREATE TABLE Products(
    Id int IDENTITY(1,1) NOT NULL,
    ProductName varchar(100) NOT NULL,
    Rate int,
    Price int NOT NULL,
    Description varchar(200),
    Avatar varchar(200) NOT NULL,
    Image1 varchar(200) NOT NULL,
    Image2 varchar(200) NOT NULL,
    Image3 varchar(200) NOT NULL,
    CategoryId int,
    Status bit NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (CategoryId) REFERENCES Categorys(Id),
   
)
CREATE TABLE Stocks(
    Id int IDENTITY(1,1) NOT NULL,
    ProductId int NOT NULL,
    CategoryId int NOT NULL,
    Quantity int NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (CategoryId) REFERENCES Categorys(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id),
)
CREATE TABLE Staffs(
    Id int IDENTITY(1,1) NOT NULL,
    UserName varchar(100) NOT NULL,
    PassWord varchar(100) NOT NULL,
    Name varchar(100) NOT NULL,
    Role int NOT NULL,
    PRIMARY KEY (Id),
)
CREATE TABLE Orders(
    Id int IDENTITY(1,1) NOT NULL,
    ProductId int NOT NULL,
    CustomerId int NOT NULL,
    Quantity int NOT NULL,
    Status varchar (200),
    Address varchar(100),
    TotalPrice int NOT NULL,
    PaymentTerm int NOT NULL,
    Phone varchar(13) NOT NULL,
    OrderDate date,
    PRIMARY KEY (Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id),
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
)
CREATE TABLE OrderDetail(
    Id int IDENTITY(1,1) NOT NULL,
    OrderId int NOT NULL,
    ProductId int NOT NULL,
    CustomerId int NOT NULL,
    Quantity int NOT NULL,
    Status varchar (200),
    Address varchar(100),
    TotalPrice int NOT NULL,
    PaymentTerm int NOT NULL,
    Phone varchar(13) NOT NULL,
    OrderDate date,
    PRIMARY KEY (Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id),
    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
)
CREATE TABLE Coupons(
    Id int IDENTITY(1,1) NOT NULL,
    Discount int NOT NULL,
    Code varchar(5) NOT NULL,
    Limit int NOT NULL,
    PRIMARY KEY (Id),
)
CREATE TABLE Wishlist(
    Id int IDENTITY(1,1) NOT NULL,
    CustomerId int ,
    ProductId int,
    PRIMARY KEY (Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id),
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
)
CREATE TABLE Comments(
 Id int IDENTITY(1,1) NOT NULL,
    ProductId int NOT NULL,
    CustomerId int NOT NULL,
    Rate int NOT NULl,
    Content varchar(200) NOT NULL
    PRIMARY KEY (Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id),
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
)

--Insert Products
insert into Products values ('Technaxx car Alarm with Charging Function', 4, 4799, 'Sensor Type: Motion Sensor, Functions: Charging, Features: Overload Protection, Maximum Frequency: 433.92 MHz, Operating Range: 26.25 ft', 'Technaxx car-1.png', 'Technaxx car-2.png', 'Technaxx car-3.png', 'Technaxx car-4.png', 1, 1)
insert into Products values ('Right Stuff® – Drilled and Slotted Brake Rotor', 5, 15799, 'Ceramic pads reduce noise fade and dust, Power Stop 1 Click Brake Kits include a complete set of cross-drilled and slotted rotors and high performance Evolution Sport Carbon Fiber, Ceramic pads', 'Right Stuff-1.png', 'Right Stuff-2.png', 'Right Stuff-3.png', 'Right Stuff-4.png', 1, 0)
insert into Products values ('R1 Concepts® – eLINE Series Plain Brake Rotors', 5, 18760, 'Package Includes 2 Drilled Slotted Brake Rotors | Precision Drilled Holes For Maximum Heat Dissipation | Countersunk To Reduce Heat Stress And Cracks | Diamond Slotted To Increase Brake Pads Bite', 'R1 Concepts-1.png', 'R1 Concepts-2.png', 'R1 Concepts-1.png', 'R1 Concepts-2.png', 1, 1)
insert into Products values ('Power Stop® – Evolution Drilled and Slotted', 4, 15489, 'Engineered to improve the braking performance of your everyday, Drilled and Slotted performance rotors for maximum cooling, Includes Drilled, Slotted Brake Rotors, Brake Pads and Installation Hardware ', 'Power Stop-1.png', 'Power Stop-2.png', 'Power Stop-3.png', 'Power Stop-4.png', 1, 0)
insert into Products values ('4000XS Drilled and Slotted Brake Rotor more details on', 5, 9959, 'Proprietary Carbon-Fiber Ceramic Materials Enhance The Strength Of A Typical Ceramic Brake Pad Compound To Handle Heavier Loads. Through On-Vehicle 3Rd Party Tests In Los Angeles', '4000XS Drilled-1.png', '4000XS Drilled-2.png', '4000XS Drilled-3.png', '4000XS Drilled-1.png', 1, 1)
insert into Products values ('AUTO-VOX CS-2 Wireless Backup Camera Kit', 4, 18999, 'Super Stable Signal – Upgraded wireless bluetooth backup camera system with Unique digital signal dual antenna, power, transmission distance, stability, durability are improved by at least 50%.', 'AUTO-VOX CS-2 -1.png', 'AUTO-VOX CS-2 -2.png', 'AUTO-VOX CS-2 -3.png', 'AUTO-VOX CS-2 -4.png', 1, 0)
insert into Products values ('5″ Monitor with 1080P Backup Camera for Truck', 5, 8399, '5" Monitor with 1080P Backup Camera for Truck, License Plate 149° Back up Rear View Kits for Reversing/ Driving Car Pickup SUV Camper Sedan, IP69 Waterproof & Clear Night Vision, Wired Xroose S3', '5Monitor-1.png', '5Monitor-2.png', '5Monitor-3.png', '5Monitor-4.png', 1, 1)
insert into Products values ('AOBEN Garage Hooks Heavy Duty, Steel Garage Storage Hooks', 4, 25779, 'Garage Hooks Heavy Duty: Robust design provides superior load bearing capacity. Heavy duty iron Garage Storage Hooks with Anti-slip Ving I Coating is more stable which can protect your items.', 'AOBEN-1.png', 'AOBEN-2.png', 'AOBEN-3.png', 'AOBEN-4.png', 2, 1)
insert into Products values ('Spyder® – Projector Headlights', 2, 521779, 'These lights are made by an OE approved and ISO certified manufacturer with the quality meet or exceed all OE standards Top quality taillights with OEM fitment for a simple bolt-on installation.', 'Spyder-1.png', 'Spyder-2.png', 'Spyder-3.png', 'Spyder-1.png', 3, 1)
insert into Products values ('Leather Honey Leather Conditioner', 4, 17879, 'COMPLETE LEATHER CARE KIT: Half-Pint Leather Conditioner, Concentrated Leather Cleaner (8oz bottle). Quickly and gently clean and condition leather car seats, truck seats, upholstery, furniture,...', 'Leather-1.png', 'Leather-2.png', 'Leather-3.png', 'Leather-1.png', 4, 1)
insert into Products values ('VISION® – 147 DAYTONA Hyper Silver', 5, 209009, 'Size: 17x8 | Bolt Pattern: 5x4.75 | Offset: 0mm | Center Bore: 83.1mm | Backspacing: 4.5, Find A Better Price? Contact Us! We ALWAYS Do Our Best To Get You The Best Deal Possible!!', 'VISION-1.png', 'VISION-2.png', 'VISION-3.png', 'VISION-3.png', 5, 1)
insert into Products values ('RADAR DIMAX AS-8 215_55R17 94V', 4, 105450, 'All-Season radial car tire for passenger cars designed to maximize fuel economy without compromising long treadlife and all season safety', 'RADAR-1.png', 'RADAR-2.png', 'RADAR-3.png', 'RADAR-4.png', 5, 1)
insert into Products values ('DEKOPRO 228 Piece Socket Wrench Auto Repair Tool', 3, 110779, 'HIGH QUALITY&STANDARDS: Forged from high-quality steel and finished in high-polish chrome,strength, durability, anti-corrosion protection.All the tools meet or exceed ANSI critical standards', 'DEKOPRO228-1.png', 'DEKOPRO228-2.png', 'DEKOPRO228-3.png', 'DEKOPRO228-4.png', 2, 0)
insert into Products values ('Spec-D® – Projector Headlights', 4, 279860, ' All of Our Items are 100% Brand New In Original Packaging! You Will Never Receive a Used Item From Us! Comes in a Pair (Driver Side Left & Passenger Side Right Included', 'Spec-D-1.png', 'Spec-D-2.png', 'Spec-D-3.png', 'Spec-D-1.png', 3, 1)
insert into Products values ('Factory Radio AM FM Radio CD Player', 3, 205000, 'Built-In Bluetooth Technology Version 4.1 Class Ii Wireless Audio Streaming With Nfc Technology For Simple One Touch Pairing, Supports A2Dp, Avrcp1.4', 'FactoryRadio-1.png', 'FactoryRadio-2.png', 'FactoryRadio-3.png', 'FactoryRadio-4.png', 4, 1)
insert into Products values ('PIRELLI TIRES® – SCORPION™ AS PLUS 3', 4, 165000, 'PIRELLI CINTURATO P7 ALL SEASON - 225/45R18 91V, BMW 4/ 3 Series - Mini Countryman/Paceman, 500 A, Run flat', 'PIRELLI-1.png', 'PIRELLI-2.png', 'PIRELLI-3.png', 'PIRELLI-4.png', 5, 1)
insert into Products values ('NICHE® – M178 TRENTO Gloss Black with Brushed Face', 5, 292009, '20X10.5, Offset42, 5X112, Gloss Black Brushed Finish, Made to be super light and impeccably durable', 'NICHE-1.png', 'NICHE-2.png', 'NICHE-3.png', 'NICHE-4.png', 5, 1)
insert into Products values ('Lmaytech Men Tools for Christmas Birthday Gift 2 Packs LED', 5, 98779, '5 Modes & Rechargeable: This rechargeable handheld flashlights Built-in rechargeable battery, rechargeable by USB ports', 'LmaytechMenTools-1.png', 'LmaytechMenTools-2.png', 'LmaytechMenTools-3.png', 'LmaytechMenTools-4.png', 2, 1)
insert into Products values ('Lumen® – Custom Sealed Beam LED Headlights', 4, 68625, 'Better heat dissipation: made of durable die-cast aluminum alloy housing and the Ribbed design heat sink maximize the heat dissipation, ensuring a longer lifespan', 'Lumen-1.png', 'Lumen-2.png', 'Lumen-3.png', 'Lumen-1.png', 3, 1)
insert into Products values ('COMSOON Upgraded Bluetooth 5.0 Receiver for Car', 5, 20000, 'This car Bluetooth adapter can be connected to non-Bluetooth car audio systems, home stereos, speakers, wired headphones via the 3.5mm AUX adapter', 'COMSOON-1.png', 'COMSOON-2.png', 'COMSOON-3.png', 'COMSOON-4.png', 4, 1)
insert into Products values ('NEXEN® – N PRIZ AH5 WITH WHITE WALL', 4, 348999, 'The NPriz AH5 has four wide, longitudinal grooves that improve drainage performance; a rigid shoulder block design to enhance cornering', 'NEXEN-1.png', 'NEXEN-2.png', 'NEXEN-3.png', 'NEXEN-5.png', 5, 1)
insert into Products values ('IRONMAN® – IMOVE GEN 3 AS', 5, 113989, 'The Ironman iMOVE GEN 3 AS is the perfect entry level option for higher horsepower sports cars and sedans delivering exceptional all-season performance', 'IRONMAN-1.png', 'IRONMAN-2.png', 'IRONMAN-3.png', 'IRONMAN-4.png', 5, 0)
insert into Products values ('Pro-Lift C-2036D Grey 36" Z-Creeper Seat ', 5, 46779, 'Thick padded cushions create a comfortable base for easy access and movement during your maintenance tasks', 'Pro-Lift C-2036D-1.png', 'Pro-Lift C-2036D-2.png', 'Pro-Lift C-2036D-3.png', 'Pro-Lift C-2036D-4.png', 2, 1)
insert into Products values ('Lumen® – Custom Headlights', 3, 402584, 'Safety reflector: the sided micro-prism reflector makes the oncoming cars or passerby quickly notice you at night, ensuring your driving safety and others', 'Lumen1-1.png', 'Lumen1-2.png', 'Lumen1-3.png', 'Lumen1-1.png', 3, 0)
insert into Products values ('Car Charger, AINOPE Smallest 4.8A', 5, 9006, 'COMPACT SIZE, FLUSH FIT: The tiny thumb-sized (1.7" (L) x 0.9" (W)) body fits most car cigarette lighters', 'AINOPE-1.png', 'AINOPE-2.png', 'AINOPE-3.png', 'AINOPE-4.png', 4, 1)
insert into Products values ('HRE FlowForm® – FT01 Tarmac', 4, 622999, 'Long a market leader in forged wheels, HRE offers its FlowForm® line of wheels featuring iconic styling and a level of quality embodied by the HRE brand', 'HRE-1.png', 'HRE-2.png', 'HRE-3.png', 'HRE-1.png', 5, 1)
insert into Products values ('FUEL® – D771 TWITCH 1PC Candy Red with Milled Accents', 5, 618000, 'These astonishing monoblock wheels would fit someone who is looking for strength, performance, and style.', 'FUEL-1.png', 'FUEL-2.png', 'FUEL-3.png', 'FUEL-4.png', 5, 1)
insert into Products values ('MAXXHAUL 70472 Solid Rubber Heavy Duty Black Wheel', 4, 15779, 'Includes built-in handle for easy placement or positioning and rubber traction pad with oil resistant surface to prevent slippage', 'MAXXHAUL-1.png', 'MAXXHAUL-2.png', 'MAXXHAUL-3.png', 'MAXXHAUL-4.png', 2, 0)
insert into Products values ('CG® – Projector Headlights', 5, 363449, 'Plug-n-Play Operation, Direct OE Fitment or Replacement for the Stock Unit, Brings a Different Appearance to Veichle that Great for Show Use or Replacement', 'CG-1.png', 'CG-2.png', 'CG-3.png', 'CG-1.png', 3, 1)
insert into Products values ('Adam’s Advanced Graphene Ceramic Coating (60ml)', 4, 113050, 'PATENT PENDING UV TRACING TECHNOLOGY! Infused with patent-pending Ceramic Glow Technology', 'Adam-1.png', 'Adam-2.png', 'Adam-3.png', 'Adam-4.png', 4, 1)
insert into Products values ('FUEL® – D764 SFJ 1PC Matte Anthracite', 4, 530000, 'They feature an exceptionally stiff construction and technical perfection.', 'FUEL-b-1.png', 'FUEL-b-2.png', 'FUEL-b-3.png', 'FUEL-b-4.png', 5, 0)
insert into Products values ('COOPER TIRES® – COBRA RADIAL G_T', 5, 73000, 'Treadwear warranty: 40,000 miles, All-Season Classic Tire, Retains characteristic look of a classic muscle car, Raised white lettering style', 'COOPER-1.png', 'COOPER-2.png', 'COOPER-3.png', 'COOPER-1.png', 5, 1)

--Insert Categorys
insert into Categorys values('Auto Safety & Security')
insert into Categorys values('Garage Tools')
insert into Categorys values('Headlights & Lighting')
insert into Categorys values('Interior Accessories')
insert into Categorys values('Tires & Wheels')

--Insert Customer
insert into Customers values ('Thai3', 'Huỳnh Ngọc Thái', 'thai@gmail.com', '13268266', 'HCM', 123, 1)
insert into Customers values ('Thai000', 'Huỳnh Ngọc Thái', 'thai@gmail.com', '051284624', 'HCM', 123, 1)

--Insert Comments
insert into Comments values(31, 1, 4, 'good')
insert into Comments values(31, 2, 2, 'Bad')
insert into Comments values(32, 2, 4, 'Good')
insert into Comments values(32, 2, 1, 'Very bad')
insert into Comments values(33, 1, 5, 'good')
insert into Comments values(33, 1, 3, 'bad')