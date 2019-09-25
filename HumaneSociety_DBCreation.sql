CREATE TABLE Employees (EmployeeId INTEGER IDENTITY (1,1) PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50), UserName VARCHAR(50), Password VARCHAR(50), EmployeeNumber INTEGER, Email VARCHAR(50));
CREATE TABLE Categories (CategoryId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE DietPlans(DietPlanId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), FoodType VARCHAR(50), FoodAmountInCups INTEGER);
CREATE TABLE Animals (AnimalId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), Weight INTEGER, Age INTEGER, Demeanor VARCHAR(50), KidFriendly BIT, PetFriendly BIT, Gender VARCHAR(50), AdoptionStatus VARCHAR(50), CategoryId INTEGER FOREIGN KEY REFERENCES Categories(CategoryId), DietPlanId INTEGER FOREIGN KEY REFERENCES DietPlans(DietPlanId), EmployeeId INTEGER FOREIGN KEY REFERENCES Employees(EmployeeId));
CREATE TABLE Rooms (RoomId INTEGER IDENTITY (1,1) PRIMARY KEY, RoomNumber INTEGER, AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId));
CREATE TABLE Shots (ShotId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50));
CREATE TABLE AnimalShots (AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId), ShotId INTEGER FOREIGN KEY REFERENCES Shots(ShotId), DateReceived DATE, CONSTRAINT AnimalShotId PRIMARY KEY (AnimalId, ShotId));
CREATE TABLE USStates (USStateId INTEGER IDENTITY (1,1) PRIMARY KEY, Name VARCHAR(50), Abbreviation VARCHAR(2));
CREATE TABLE Addresses (AddressId INTEGER IDENTITY (1,1) PRIMARY KEY, AddressLine1 VARCHAR(50), City VARCHAR(50), USStateId INTEGER FOREIGN KEY REFERENCES USStates(USStateId),  Zipcode INTEGER); 
CREATE TABLE Clients (ClientId INTEGER IDENTITY (1,1) PRIMARY KEY, FirstName VARCHAR(50), LastName VARCHAR(50), UserName VARCHAR(50), Password VARCHAR(50), AddressId INTEGER FOREIGN KEY REFERENCES Addresses(AddressId), Email VARCHAR(50));
CREATE TABLE Adoptions(ClientId INTEGER FOREIGN KEY REFERENCES Clients(ClientId), AnimalId INTEGER FOREIGN KEY REFERENCES Animals(AnimalId), ApprovalStatus VARCHAR(50), AdoptionFee INTEGER, PaymentCollected BIT, CONSTRAINT AdoptionId PRIMARY KEY (ClientId, AnimalId));

INSERT INTO Categories VALUES ('Fire');
INSERT INTO Categories VALUES ('Water');
INSERT INTO Categories VALUES ('Mythical');
INSERT INTO Categories VALUES ('Normal');
INSERT INTO Categories VALUES ('Sickly');

INSERT INTO Animals VALUES ('Blue-eyes Pink Dragon',12000, 2000,'nice', 0, 0, 'Alpha-male','pending', 3, 1, 2);
INSERT INTO Animals VALUES ('Liger',240,24,'mean', 0, 0, 'Alpha-female','pending',4, 5, 3);
INSERT INTO Animals VALUES ('Unicorn',2000,100000,'legendary', 1, 1, 'Alpha-female','auction', 3, 2, 5);
INSERT INTO Animals VALUES ('Behemoth',8000, 4000,'Adamant', 0, 0, 'Alpha-male','pending',3, 3, 1);
INSERT INTO Animals VALUES ('Charizard',3600, 36,'Jolly', 1, 1, 'Alpha-male','pending',3, 4, 4);

INSERT INTO DietPlans VALUES('NomNomBacon','Carnivorous', 3);
INSERT INTO DietPlans VALUES('VeggieSlush','herbivorous', 2);
INSERT INTO DietPlans VALUES('MeatLoversMealPlan','Carnivorous', 3);
INSERT INTO DietPlans VALUES('BasicLeftovers','Omnivorous', 10);
INSERT INTO DietPlans VALUES('DelicatePaletePlates','Omnivorous', 1);


INSERT INTO Employees VALUES ('Andrew','Malkasian','LonelyBoyForever24','coolbeans',1, 'LonelyBoyForever24@gmail.com');
INSERT INTO Employees VALUES ('Greg','Manthey','EpicVegan','veggiesRlife',2, 'EpicVegan@gmail.com');
INSERT INTO Employees VALUES ('Jake','Gambino','TheGreatBambino','Jgambino', 3, 'TheGreatBambino@msn.com');
INSERT INTO Employees VALUES ('Charles','King','KingCharles4','Wifesname+kidsbirthday',4, 'KingCharles4@yahoo.com');
INSERT INTO Employees VALUES ('Hannah','Bingham','Jakedrools','Ihateeveryone',5,'dontbotherme@onthecouch.org');

INSERT INTO Clients VALUES ('Dave','Quitter','ImaQuitter','HTMLCSS',null, 'ImaQuitter@wix.com');
INSERT INTO Clients VALUES ('Najmeel','Where','TheFoodPlace','Illbeback',null, 'TheFoodPlace@gmail.com');
INSERT INTO Clients VALUES ('Joyce','Malkasian','YourMomHasAUserName','AndAPassWord',null, 'Jmalkasian@makarios.com');
INSERT INTO Clients VALUES ('Bob','Barker','GameShowMaster','TheRightPrice',null, 'GameShowMaster@yahoo.com');
INSERT INTO Clients VALUES ('Justin','Tucker','RaveMaster','upright12345',null,'RaveMaster@gmail.com');

INSERT INTO Rooms VALUES (10 , 6);
INSERT INTO Rooms VALUES (12 , 7);
INSERT INTO Rooms VALUES (14 , 8);
INSERT INTO Rooms VALUES (16 , 9);
INSERT INTO Rooms VALUES (18 , 10);
INSERT INTO Rooms VALUES (20 , null);
INSERT INTO Rooms VALUES (22 , null);
INSERT INTO Rooms VALUES (24 , null);
INSERT INTO Rooms VALUES (26 , null);
INSERT INTO Rooms VALUES (28 , null);







INSERT INTO USStates VALUES('Alabama','AL');
INSERT INTO USStates VALUES('Alaska','AK');
INSERT INTO USStates VALUES('Arizona','AZ');
INSERT INTO USStates VALUES('Arkansas','AR');
INSERT INTO USStates VALUES('California','CA');
INSERT INTO USStates VALUES('Colorado','CO');
INSERT INTO USStates VALUES('Connecticut','CT');
INSERT INTO USStates VALUES('Delaware','DE');
INSERT INTO USStates VALUES('Florida','FL');
INSERT INTO USStates VALUES('Georgia','GA');
INSERT INTO USStates VALUES('Hawaii','HI');
INSERT INTO USStates VALUES('Idaho','ID');
INSERT INTO USStates VALUES('Illinois','IL');
INSERT INTO USStates VALUES('Indiana','IN');
INSERT INTO USStates VALUES('Iowa','IA');
INSERT INTO USStates VALUES('Kansas','KS');
INSERT INTO USStates VALUES('Kentucky','KY');
INSERT INTO USStates VALUES('Louisiana','LA');
INSERT INTO USStates VALUES('Maine','ME');
INSERT INTO USStates VALUES('Maryland','MD');
INSERT INTO USStates VALUES('Massachusetts','MA');
INSERT INTO USStates VALUES('Michigan','MI');
INSERT INTO USStates VALUES('Minnesota','MN');
INSERT INTO USStates VALUES('Mississippi','MS');
INSERT INTO USStates VALUES('Missouri','MO');
INSERT INTO USStates VALUES('Montana','MT');
INSERT INTO USStates VALUES('Nebraska','NE');
INSERT INTO USStates VALUES('Nevada','NV');
INSERT INTO USStates VALUES('New Hampshire','NH');
INSERT INTO USStates VALUES('New Jersey','NJ');
INSERT INTO USStates VALUES('New Mexico','NM');
INSERT INTO USStates VALUES('New York','NY');
INSERT INTO USStates VALUES('North Carolina','NC');
INSERT INTO USStates VALUES('North Dakota','ND');
INSERT INTO USStates VALUES('Ohio','OH');
INSERT INTO USStates VALUES('Oklahoma','OK');
INSERT INTO USStates VALUES('Oregon','OR');
INSERT INTO USStates VALUES('Pennsylvania','PA');
INSERT INTO USStates VALUES('Rhode Island','RI');
INSERT INTO USStates VALUES('South Carolina','SC');
INSERT INTO USStates VALUES('South Dakota','SD');
INSERT INTO USStates VALUES('Tennessee','TN');
INSERT INTO USStates VALUES('Texas','TX');
INSERT INTO USStates VALUES('Utah','UT');
INSERT INTO USStates VALUES('Vermont','VT');
INSERT INTO USStates VALUES('Virginia','VA');
INSERT INTO USStates VALUES('Washington','WA');
INSERT INTO USStates VALUES('West Virgina','WV');
INSERT INTO USStates VALUES('Wisconsin','WI');
INSERT INTO USStates VALUES('Wyoming','WY');