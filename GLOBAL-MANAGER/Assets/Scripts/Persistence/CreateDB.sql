-- Create COUNTRY table
CREATE TABLE IF NOT EXISTS "COUNTRY" (
	"Name"	TEXT,
	"PowerDistance"	INTEGER NOT NULL,
	"Individualism"	INTEGER NOT NULL,
	"Masculinity"	INTEGER NOT NULL,
	"UncertantyAvoidance"	INTEGER NOT NULL,
	"LongTermOrientation"	INTEGER NOT NULL,
	"Indulgence"	INTEGER NOT NULL,
	"TimeZone"	REAL NOT NULL,
	"Salary"	REAL NOT NULL,
	"Instability"	INTEGER,
	"Latitude"	REAL,
	"Longitude"	REAL,
	PRIMARY KEY("Name")
);

-- Create LANGUAGE table
CREATE TABLE IF NOT EXISTS "LANGUAGE" (
	"Name"	TEXT,
	PRIMARY KEY("Name")
);

-- Create SITE table
CREATE TABLE IF NOT EXISTS "SITE" (
	"CodSite"	INTEGER,
	"Country"	TEXT NOT NULL,
	"Name"	TEXT NOT NULL,
	"TeamSize"	INTEGER NOT NULL,
	"LevelCommonLanguage"	TEXT NOT NULL,
	FOREIGN KEY("Country") REFERENCES "COUNTRY"("Name"),
	PRIMARY KEY("CodSite" AUTOINCREMENT)
);

-- Create COMMUNICATION table
CREATE TABLE IF NOT EXISTS "COMMUNICATION" (
	"Name"	TEXT,
	"Type"	TEXT NOT NULL,
	PRIMARY KEY("Name")
);

-- Create CHARACTERISTICS table
CREATE TABLE IF NOT EXISTS "CHARACTERISTICS" (
	"CodCharacteristics"	INTEGER,
	"WorkingTimeOverlap"	TEXT NOT NULL,
	"LanguageDifference"	TEXT NOT NULL,
	"CulturalDifference"	TEXT NOT NULL,
	"InestabilityCountries"	TEXT NOT NULL,
	"CostumerProximity"	TEXT NOT NULL,
	"Communication"	TEXT NOT NULL,
	"SitesNumber"	TEXT NOT NULL,
	PRIMARY KEY("CodCharacteristics" AUTOINCREMENT)
);

-- Create PLAYER table
CREATE TABLE IF NOT EXISTS "PLAYER" (
	"Username"	TEXT,
	"Age"	INTEGER,
	"UserLevel"	TEXT NOT NULL,
	"Score"	INTEGER,
	"NumProjects"	INTEGER,
	"IsMan"	INTEGER,
	PRIMARY KEY("Username")
);

-- Create GAME table
CREATE TABLE IF NOT EXISTS "GAME" (
	"CodGame"	INTEGER,
	"Player"	TEXT NOT NULL,
	"Language"	TEXT NOT NULL,
	"CustomerCountry"	TEXT NOT NULL,
	"Characteristics"	INTEGER NOT NULL,
	"NumSites"	INTEGER NOT NULL,
	"ProjectDifficulty"	TEXT NOT NULL,
	"InitialBudget"	REAL NOT NULL,
	"InitialDuration"	REAL NOT NULL,
	"StressValue"	REAL,
	"ProgressValue"	REAL,
	"BudgetValue"	REAL,
	"DurationValue"	REAL,
	"TotalNegativeEvents"	INTEGER,
	"CorrectNegativeEvents"	INTEGER,
	PRIMARY KEY("CodGame" AUTOINCREMENT),
	FOREIGN KEY("CustomerCountry") REFERENCES "COUNTRY"("Name"),
	FOREIGN KEY("Player") REFERENCES "PLAYER"("Username"),
	FOREIGN KEY("Language") REFERENCES "LANGUAGE"("Name"),
	FOREIGN KEY("Characteristics") REFERENCES "CHARACTERISTICS"("CodCharacteristics")
);

-- Create relationship between SITE and GAME
CREATE TABLE IF NOT EXISTS "SiteGame" (
	"Game"	INTEGER,
	"Site"	INTEGER,
	"MainSite"	INTEGER,
	FOREIGN KEY("Game") REFERENCES "GAME"("CodGame"),
	FOREIGN KEY("Site") REFERENCES "SITE"("CodSite"),
	PRIMARY KEY("Game","Site")
);

-- Create relationship between COUNTRY and LANGUAGE
CREATE TABLE IF NOT EXISTS "Speak" (
	"Country"	TEXT,
	"Language"	TEXT,
    "Official"	INTEGER NOT NULL,
	PRIMARY KEY("Country","Language"),
	FOREIGN KEY("Country") REFERENCES "COUNTRY"("Name"),
	FOREIGN KEY("Language") REFERENCES "LANGUAGE"("Name")
);

-- Create relationship between GAME, SITE, SITE and COMMUNICATION
CREATE TABLE IF NOT EXISTS "Communicate" (
	"Game"	INTEGER,
	"Site1"	INTEGER,
	"Site2"	INTEGER,
	"Communication"	TEXT,
	PRIMARY KEY("Game","Site1","Site2","Communication"),
	FOREIGN KEY("Communication") REFERENCES "COMMUNICATION"("Name"),
	FOREIGN KEY("Game") REFERENCES "GAME"("CodGame"),
	FOREIGN KEY("Site1") REFERENCES "SITE"("CodSite"),
	FOREIGN KEY("Site2") REFERENCES "SITE"("CodSite")
);