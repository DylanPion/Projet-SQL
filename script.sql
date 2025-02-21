-- Insertion des catégories
INSERT INTO Categories (Name) VALUES 
('Roman'),
('Science-Fiction'),
('Policier'),
('Biographie'),
('Histoire'),
('Jeunesse');

-- Insertion des éditeurs
INSERT INTO Publishers (Name, ContactEmail, FoundedYear) VALUES 
('Gallimard', 'contact@gallimard.fr', 1911),
('Hachette', 'contact@hachette.fr', 1826),
('Flammarion', 'contact@flammarion.fr', 1875),
('Albin Michel', 'contact@albin-michel.fr', 1900);

-- Insertion des utilisateurs
INSERT INTO Users (Name, Email, Role) VALUES 
('Jean Dupont', 'jean.dupont@email.fr', 'Administrateur'),
('Marie Martin', 'marie.martin@email.fr', 'Lecteur'),
('Pierre Durant', 'pierre.durant@email.fr', 'Lecteur'),
('Sophie Bernard', 'sophie.bernard@email.fr', 'Lecteur');

-- Insertion des livres
INSERT INTO Books (Title, Author, PublishedYear, ISBN, CategoryId, PublisherId) VALUES 
('Les Misérables', 'Victor Hugo', 1862, '978-2-07-040089-1', 1, 1),
('Dune', 'Frank Herbert', 1965, '978-2-221-00252-4', 2, 2),
('Le Petit Prince', 'Antoine de Saint-Exupéry', 1943, '978-2-07-040850-4', 6, 1),
('L''Étranger', 'Albert Camus', 1942, '978-2-07-036002-4', 1, 1),
('Germinal', 'Émile Zola', 1885, '978-2-253-00725-4', 1, 2);

-- Insertion des prêts
INSERT INTO Loans (BookId, UserId, BorrowDate, ReturnDate) VALUES 
(1, 2, '2024-01-15', NULL),
(2, 3, '2024-02-01', '2024-02-15'),
(3, 4, '2024-02-10', NULL),
(4, 2, '2024-01-20', '2024-02-20'),
(5, 3, '2024-02-05', NULL);