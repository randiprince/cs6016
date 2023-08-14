# Lab 9: Indexes

### Part 1 - Selecting Indexes
A database contains the following table for former-employee records:
```
Here we would need one additonal index of Index(StartDate, EndDate)
because we want to be able to search for a specific StartDate or for a
StartDate and a certain EndDate. The multicolumn index is good for searching both column using AND
```
A database contains the following table for tracking student grades in classes:
```
For these first two queries, we would need additional index Index(Grade)
so that we can search more quickly by grade because it is not a primary key
```

Using the same grade database, but now the common queries are:
```
For these second two queries, we would still just need additional index Index(Grade)
because className already has a primary index, so we still just need to be able to search by grade
```

Queries on the chess database:
```
for these queries we should have an addtional index on Players.Elo so we can search by Elo and another index on
Games.WhitePlayer so we can join by WhitePlayer 
```

Queries on the public Library database
```
Serial is the primary key aka the primary index of both the Inventory and CheckedOut tables. 
Knowing this, no additional indexes are needed.
```

More library queries:
```
again, both Inventory and CheckedOut have primary key Serial in common, so we'd only need additional
index of Index(CheckedOut.CardNum) so we can also query by that.
For the second query, CheckedOut and Patrons share the column CardNum, and CardNum is the primary index of Patrons,
so Index(CheckedOut.CardNum) will allow us to join so nothing additional is needed.
```

Still more library queries:
```
This query would cause joining of Titles and Inventory. Serial is the primary key of Inventory and ISBN is the primary key of Titles,
so the join would benefit from creating an index on Index(Inventory.ISBN)
```

### Part 2 - B+ Tree Index Structures
How many rows of the table can be placed into the first leaf node of the primary index before it will split?
- studentID (int) (primary key) would take up 4 bytes
- className (varchar(10)) (primary key) would take up 10 bytes
- Grade (char(1)) would take up 1 byte

so one row is 15 bytes. 4096 / 15 is **273 rows** because leaf nodes hold whole rows

#

What is the maximum number of keys stored in an internal node of the primary index? (Remember to ignore pointer space. Remember that internal nodes have a different structure than leaf nodes.)
- the key is studentID (would take up 4 bytes) and className (10 bytes)
- 14 bytes per key
- internal nodes only hold keys
so the max number of keys is 4096 / 14 = **292 is the max number of keys**

#

What is the maximum number of rows in the table if the primary index has a height of 1? (A tree of height 1 has 2 levels and requires exactly one internal node)
- from above we know that a tree of height 1 will have 292 nodes + 1 child node.
- Each of the leaf nodes will have 273 rows in it

so the max number of rows is **293 * 273 = 79,989 rows**

#

What is the minimum number of rows in the table if the primary index has a height of 1? (A tree of height 1 has 2 levels). The minimum capacity of a node in a B+ tree is 50%, unless it is the only internal/leaf node. The minimum number of children of a root node is 2.
- one node can have 273 rows
- more than this will cause a split 

so 273 + 1 gives us **274 to be the minimum number of rows for a height of 1**

# 

If there is a secondary index on Grade, what is the maximum number of entries a leaf node can hold in the secondary index?
- secondary index means the grade char that takes one byte would have to be accounted 
- 4 bytes + 10 bytes from the primary keys + 1 byte from the index

so 4096 / 15 = **maximum of 273 rows**

### Another table
What is the maximum number of leaf nodes in the primary index if the table contains 48 rows?
- so 4096/128 = 32 rows can be held per lead node
- leaf node needs to be 50% full so each should hold 32/2 = 16 rows
- 48 / 16 = 3

so **maximum of 3 rows are needed**

#

What is the minimum number of leaf nodes in the primary index if the table contains 48 rows?
- 32 rows per leaf node
- 48 / 32 is 1.5

so need **minimum of 2 nodes**
