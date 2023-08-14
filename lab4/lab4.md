# Part 3 - Simple Retrieval Queries
1. Get the Titles of all books by < Author >
```mysql
SELECT Title
FROM Titles
WHERE Author = "<Author>";
```

2. Get Serial numbers of all books by < Author >
```mysql
SELECT Serial FROM Inventory NATURAL JOIN Titles WHERE Author = "<Author>";
```

3. Get the Titles of all books checked out by < Patron’s name >
```mysql
SELECT Title FROM CheckedOut natural join Inventory natural join Patrons natural join Titles WHERE Patrons.Name = "<Patron’s name>";
```

4. Get phone number(s) of anyone with < Title > checked out
```mysql
SELECT Phone FROM CheckedOut natural join Inventory natural join Phones natural join Titles WHERE Titles.Title = "<Title>";
```

# Part 4 - Intermediate Retrieval Queries
1. Find the Titles of the library's oldest < N > books. Assume the lowest serial number is the oldest book.
```mysql
SELECT Title
FROM Title
NATURAL JOIN Inventory 
ORDER BY Inventory.Serial ASC
LIMIT <N>;
```

2. Find the name of the person who has checked out the most recent book. Assume higher serial numbers are newer. Note that this query is not concerned with the absolute highest serial number, it is concerned with the highest one that has been checked out.
```mysql
SELECT Name
FROM Patrons
NATURAL JOIN Inventory 
NATURAL JOIN CheckedOut
ORDER BY Serial DESC
LIMIT 1;
```

3. Find the phone number(s) of anyone who has not checked out any books. If a phone number belongs to two Patrons where one of them could have checked out a book, then that phone number should not be included in the output.
```mysql
SELECT Phone
FROM Phones
WHERE Phone NOT IN (
    SELECT Phone
    FROM Phones
    NATURAL JOIN CheckedOut 
    NATURAL JOIN Patrons
)
```

4. The library wants to expand the number of unique selections in its inventory, thus, it must know the ISBN and Title of all books that it owns at least one copy of. Create a query that will return the ISBN and Title of every book in the library, but will not return the same book twice.
```mysql
SELECT distinct Title, ISBN
FROM Titles
NATURAL JOIN Inventory;
```

# Part 5 - Chess Queries
1. Find the names of any player with an Elo rating of 2850 or higher.
```mysql
SELECT Name
FROM Players
WHERE Elo >= 2850
```

2. Find the names of any player who has ever played a game as white.
```mysql
SELECT DISTINCT Name
FROM Players
CROSS JOIN Games
WHERE Players.pID = Games.WhitePlayer
```

3. Find the names of any player who has ever won a game as white.
```mysql
SELECT DISTINCT Name
FROM Players
CROSS JOIN Games
WHERE Players.pID = Games.WhitePlayer
AND Games.Result = 'W'
```

4. Find the names of any player who played any games in 2018.
```mysql
SELECT DISTINCT Name
FROM Players
JOIN (
    SELECT WhitePlayer, BlackPlayer
    FROM Games 
    CROSS JOIN Events
    WHERE YEAR(Events.Date) = 2018
    AND Games.eID = Events.eID
) AS P 
WHERE Players.pID = P.WhitePlayer
OR Players.pID = P.BlackPlayer
```

5. Find the names and dates of any event in which Magnus Carlsen lost a game.
```mysql
SELECT Name, Date
FROM Events
JOIN (
    SELECT DISTINCT eID
    FROM Games
    JOIN (
        SELECT pID 
        FROM Players
        WHERE Name = 'Carlsen, Magnus'
    ) AS MCID
    WHERE (MCID.pID = Games.WhitePlayer AND Games.Result = 'B')
    OR (MCID.pID = Games.BlackPlayer AND Games.Result = 'W')
) AS MCeID
WHERE Events.eID = MCeID.eID
```

6. Find the names of all opponents of Magnus Carlsen. An opponent is someone who he has played a game against. Hint: Both Magnus and his opponents could play as white or black.
```mysql
SELECT DISTINCT Name
FROM Players
JOIN (
    SELECT WhitePlayer, BlackPlayer
    FROM Games
    JOIN (
        SELECT pID 
        FROM Players
        WHERE Name = 'Carlsen, Magnus'
    ) AS MCID
    ON Games.BlackPlayer = MCID.pID
    OR Games.WhitePlayer = MCID.pID
) AS opp
WHERE (
    (WhitePlayer = Players.pID OR
    BlackPlayer = Players.pID) AND
    Name != 'Carlsen, Magnus'
)
```