-- Insert the data of the table LANGUAGE
INSERT OR IGNORE INTO LANGUAGE(Name) 
    VALUES('English'), ('Spanish'), ('Italian'), ('German'), ('French'),
          ('Dutch'), ('Mandarin'), ('Mongolian'), ('Danish'), ('Hindi'),
          ('Indonesian'), ('Japanese'), ('Portuguese'), ('Russian'), ('Korean');

-- Insert the data of the table COUNTRY
INSERT OR IGNORE INTO COUNTRY("Name", "PowerDistance", "Individualism", "Masculinity", "UncertantyAvoidance", "LongTermOrientation", "Indulgence", "TimeZone", "Salary", "Instability", "Latitude", "Longitude") 
    VALUES ('Argentina', '49', '46', '56', '86', '20', '62', '-3.0', '4.35', '0', '-34.0', '-64.0'),
           ('Australia', '38', '90', '61', '51', '21', '71', '10.0', '28.4', '0', '-25.0', '135.0'),
           ('Belgium', '65', '75', '54', '94', '82', '57', '1.0', '22.52', '0', '50.75', '4.5'),
           ('Canada', '39', '80', '52', '48', '36', '68', '-5.0', '26.98', '0', '60.10867', '-113.64258'),
           ('China', '80', '20', '66', '30', '87', '24', '8.0', '14.33', '1', '35.0', '105.0'),
           ('Denmark', '18', '74', '16', '23', '35', '70', '1.0', '40.67', '0', '56.0', '10.0'),
           ('France', '68', '71', '43', '86', '63', '48', '1.0', '23.08', '0', '46.0', '2.0'),
           ('Germany', '35', '67', '66', '65', '83', '40', '1.0', '27.7', '0', '51.5', '10.5'),
           ('India', '77', '48', '56', '40', '51', '26', '5.5', '3.31', '1', '22.0', '79.0'),
           ('Indonesia', '78', '14', '46', '48', '62', '38', '7.0', '4.16', '1', '-5.0', '120.0'),
           ('Ireland', '28', '70', '68', '35', '24', '65', '1.0', '25.39', '0', '52.865196', '-7.97946'),
           ('Italy', '50', '76', '70', '75', '61', '30', '1.0', '17.31', '0', '42.83333', '12.83333'),
           ('Japan', '54', '46', '95', '92', '88', '42', '9.0', '22.12', '0', '35.68536', '139.75309'),
           ('Mexico', '81', '30', '69', '82', '24', '97', '-6.0', '9.44', '1', '23.0', '-102.0'),
           ('New Zealand', '22', '79', '58', '49', '33', '75', '12.0', '33.3', '0', '-40.900558', '174.885971'),
           ('Portugal', '63', '27', '31', '99', '28', '33', '0.0', '10.39', '0', '39.6945', '-8.13057'),
           ('Russia', '93', '39', '36', '95', '81', '20', '3.0', '8.9', '1', '60.0', '100.0'),
           ('South Korea', '60', '18', '39', '85', '100', '29', '9.0', '19.39', '0', '36.5', '127.75'),
           ('Spain', '57', '51', '42', '86', '48', '44', '1.0', '19.05', '0', '40.0', '-4.0'),
           ('United Kingdom', '35', '89', '66', '35', '51', '69', '0.0', '29.03', '0', '55.378052', '-3.435973'),
           ('United States', '40', '91', '62', '46', '26', '68', '-5.0', '42.16', '0', '37.09024', '-95.712891');

-- Insert the data of the table Speak
INSERT OR IGNORE INTO Speak(Country, Language, Official)
    VALUES('Argentina', 'Spanish', 1), ('Argentina', 'English', 0), ('Argentina', 'Italian', 0), ('Argentina', 'German', 0),
          ('Argentina', 'French', 0), ('Australia', 'English', 1), ('Belgium', 'Dutch', 1), ('Belgium', 'French', 1),
          ('Belgium', 'German', 1), ('Canada', 'English', 1), ('Canada', 'French', 1), ('China', 'Mandarin', 1),
          ('China', 'Mongolian', 0), ('Denmark', 'Danish', 1), ('Denmark', 'German', 0), ('Denmark', 'English', 0),
          ('France', 'French', 1), ('Germany', 'German', 1), ('India', 'Hindi', 1), ('India', 'English', 1),
          ('Indonesia', 'Indonesian', 1), ('Indonesia', 'English', 0), ('Indonesia', 'Dutch', 0), ('Ireland', 'English', 1),
          ('Italy', 'Italian', 1), ('Italy', 'German', 0), ('Italy', 'French', 0), ('Japan', 'Japanese', 1),
          ('Mexico', 'Spanish', 1), ('New Zealand', 'English', 1), ('Portugal', 'Portuguese', 1), ('Russia', 'Russian', 1),
          ('South Korea', 'Korean', 1), ('South Korea', 'English', 0), ('Spain', 'Spanish', 1), ('United Kingdom', 'English', 1),
          ('United States', 'English', 1), ('United States', 'Spanish', 0);
          
-- Insert the data of the table COMMUNICATION
INSERT OR IGNORE INTO COMMUNICATION(Name, Type)
    VALUES('Phone', 'Synchronous'), ('Whatsapp', 'Asynchronous'), ('Skype', 'Synchronous'),
          ('Forum', 'Asynchronous'), ('Microsoft Teams', 'Synchronous'), ('E-Mail', 'Asynchronous');
