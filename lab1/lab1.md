# Part 1: English to schema

### A grocery store needs to track an inventory of products for sale. It has zero or more of each type of product for sale, and needs to track the quantity and price for each product. A product has a name and a "stock keeping unit" (SKU) (hint: this is a real thing, you may google it). Remember that a valid table instance can't have duplicate rows - the store does not care about differentiating between individual items of the same product type, but it does want to be able to count them.
``` txt
ProductInventory [__SKU (string)__, name (string), quantity (integer), price (real)]
```

### Consider the grocery store database from the previous problem, but with a few differences: Now we don't care about tracking quantity, but we do want to track which aisle(s) the product is to be displayed on. Sometimes a product is displayed on more than one aisle in special display racks, but the product can not have multiple display cases per aisle. You may copy the relevant parts from your previous answer, but they will need modifications/additions.

``` txt
ProductLocation [__SKU (string)__, __aisle (integer)__, name (string), price (real)]
```

### A car has a make, model, year, color, and VIN (vehicle identification number). A salesperson has a name and a social security number, and is responsible for trying to sell zero or more cars. A car dealership has an inventory of cars and a set of salespeople. It needs to keep track of which car(s) each salesperson is trying to sell. More than one salesperson can be assigned to any given car, but a car does not necessarily have any salespeople assigned to it.

``` txt
Cars [__VIN (string)__, make (string), model (string), year (integer), color (string)]

Salesperson [__SSN (integer)__, Name (string)]

DealershipInventory [__carVIN (string)__, __employeeSSN (interger)__]
```

# Part 2: SQL Table Declarations
```mysql
CREATE TABLE Patrons (
  Name VARCHAR(75),
  CardNum INT,
  PRIMARY KEY(CardNum)
)

CREATE TABLE Inventory (
    Serial INT,
    ISBN VARCHAR(13),
    PRIMARY KEY(Serial),
    FOREIGN KEY(ISBN) REFERENCES Titles(ISBN)
)

CREATE TABLE CheckedOut (
    CardNum INT,
    SERIAL INT,
    PRIMARY KEY(CardNum, Serial),
    FOREIGN KEY(CardNum) REFERENCES Patrons(CardNum),
    FOREIGN KEY(Serial) REFERENCES Inventory(Serial)
)

CREATE TABLE Phones (
    CardNum INT,
    Phone VARCHAR(8),
    PRIMARY KEY(CardNum, Phone),
    FOREIGN KEY(CardNum) REFERENCES Patrons(CardNum)
)

CREATE TABLE Titles (
    ISBN VARCHAR(13),
    Title VARCHAR(100),
    Author VARCHAR(50),
    PRIMARY KEY(ISBN)
)
```

# Part 3 - Fill in Tables

**Cars:**
``` txt
Cars [__VIN (string)__, make (string), model (string), year (integer), color (string)]
```
| VIN | Make | Model | Year | Color |
| ---- | ---- | ---- | ---- | ---- |
| ABCD123P6Q | Honda | Pilot | 2015 | Blue |
| ZDGH909P7T | Hyundai | Sonata | 2004 | Black |
| ABCD123P6Q | Subaru | Outback | 2023 | Green |
| ABCD123P6Q | Jeep | Wrangler | 2022 | Silver |
| ABCD123P6Q | Toyota | Camry | 2019 | Red |

**Salesperson:**
``` txt
Salesperson [__SSN (integer)__, Name (string)]
```
| SSN | Name | 
| ---- | ---- |
| 789906565 | Randi |
| 534095932 | Ryan |
| 009876543 | Hugo |
| 123456789 | Marlon |
| 260537128 | Squishy |

**DealershipInventory:**
```txt
DealershipInventory [__VIN (string)__, __SSN (interger)__]
```
| VIN | SSN | 
| ---- | ---- |
| 789906565 | Randi |
| 789906565 | Ryan |
| 789906565 | Hugo |
| 123456789 | Randi |
| 260537128 | Squishy |
| 534095932 | Marlon |
| 009876543 |  |
| 123456789 | Marlon |
| 260537128 | Squishy |

# Part 4 - Keys and Superkeys
| Attribute Sets | Superkey? | Proper Subsets | Key? |
| -------------- | --------- | -------------- | ---- |
| {A1} | No | {} | No |
| {A2} | No | {} | No |
| {A3} | No | {} | No |
| {A1, A2} | Yes | {A1}, {A2} | Yes |
| {A1, A3} | No | {A1}, {A3} | No |
| {A2, A3} | No | {A2}, {A3} | No |
| {A1, A2, A3} | Yes | {A1}, {A2}, {A3}, {A1} {A2}, {A1} {A3}, {A2} {A3} | No |

# Part 5 - Abstract Reasoning
- If {x} is a superkey, then any set containing x is also a superkey.
    - 

    - **True**
    - If x is a superkey, every vale of x is unqiue, meaning any other set you combine with x will also be unqiue.

- If {x} is a key, then any set containing x is also a key.
    - 

    - **False**
    - A key is a key if it is a superkey and if no proper subset of the key is a super key. So if {x} is a key, then it is also a superkey by definition. So if any other set containing x like {x, y} is not a key because {x} is a proper subset of {x, y} and a superkey, making {x, y} not a key.

- If {x} is a key, then {x} is also a superkey.
    - 

    - **True**
    - By definition a key is a key if it is also a superkey.

- If {x, y, z} is a superkey, then one of {x}, {y}, or {z} must also be a superkey.
    - 

    - **False**
    - In order for {x, y, z} to be a superkey, all values for each tuple need to be unqiue, but that doesn't mean the separate columns themselves need to be unqiue. In the following table, we can see that each of the colums {x}, {y}, and {z} are not superkeys themselves, but each tuple of {x, y, z} together are unqiue

        | x | y | z |
        | --- | --- | --- |
        | 1 | 3 | 5
        | 1 | 4 | 5
        | 2 | 4 | 6

- If an entire schema consists of the set {x, y, z}, and if none of the proper subsets of {x, y, z} are keys, then {x, y, z} must be a key.
    - 

    - **True**
    - If none of the subsets are keys, then {x, y, z} must be the minimal subset to uniquely identify the relation.
