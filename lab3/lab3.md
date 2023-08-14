# Lab 3: Relational Algebra

## Part 1 - Joins:
$T1 \bowtie _{T1.A = T2.A} T2$ 
| A | Q | R | B | C |
| -- | -- | -- | -- | -- |
| 20 | a | 5 | b | 6 |
| 20 | a | 5 | b | 5 |

$T1 \bowtie _{T1.Q = T2.B} T2$ 
| T1.A | Q | R | T2.A | B | C |
| -- | -- | -- | -- | -- | -- |
| 25 | b | 8 | 20 | b | 6 |
| 25 | b | 8 | 20 | b | 5 |

$T1 \bowtie T2$ 
| A | Q | R | B | C |
| -- | -- | -- | -- | -- |
| 20 | a | 5 | b | 6 |
| 20 | a | 5 | b | 5 |

$T1 \bowtie _{T1.A = T2.A \wedge T1.R = T2.C} T2$
| A | Q | R | B | C |
| -- | -- | -- | -- | -- |
| 20 | a | 5 | b | 5 |

## Part 2 - Chess Queries:
#
Find the names of any player with an Elo rating of 2850 or higher:

$\Pi _{\text Name} (\sigma_{\text{Elo} > 2850}(\text{Players}))$
#
Find the names of any player who has ever played a game as white:

$\Pi _{\text Players} (\sigma_{\text{pID = wpID}} \text{(Players X Games)})$
#
Find the names of any player who has ever won a game as white:

$\Pi _{\text Players} (\sigma_{\text{pID = wpID}\wedge \text{Result = '1-0'}} \text{(Players X Games)})$
#
Find the names of any player who played any games in 2018:

$\Pi_{\text Players.Name}(\sigma_{pID = wpID \vee pID = bpID}(Players X (\sigma _{Games.eID = Events.eID \wedge Year=2018}(Games X Events))))$
#

Find the names and dates of any event in which Magnus Carlsen lost a game:
$\sigma (MCID, (\Pi _{pID}(\sigma_{Name=Mangus Carlsen} (Players))))$

$\sigma (MCeID, (\Pi _{eID}(\sigma_{MCID = wpID \wedge result = '0-1} \vee_{MCID = bpID \wedge result = '1-0} (Games))))$

$\Pi_{Name, Year}(\sigma_{MCeID =eID} (Events))$

#
Find the names of all opponents of Magnus Carlsen. An opponent is someone who he has played a game against. Hint: Both Magnus and his opponents could play as white or black:

$\sigma (MCID, (\Pi _{pID}(\sigma_{Name=Mangus Carlsen} (Players))))$
$\sigma (MCWhiteOppID, (\Pi _{wpID}(\sigma_{bpID=MCID} (Games))))$
$\sigma (MCWhiteOppName, (\Pi _{Name}(\sigma_{MCWhiteOppID=pID} (Players))))$
$\sigma (MCBlackOppID, (\Pi _{bpID}(\sigma_{wpID=MCID} (Games))))$
$\sigma (MCBlackOppName, (\Pi _{Name}(\sigma_{MCBlackOppID=pID} (Players))))$ 

$MCWhiteOppName \space\cup\space MCBlackOppName$


## Part 3 - LMS Queries
### Part 3.1:
| Name VARCHAR(255) |
| ---- |
| Hermione |
| Harry |

This query is asking us to pull out the names of the students who did not get the grade C.

### Part 3.2:
| S2.Name VARCHAR(255) |
| ---- |
| Hermione |

This query is to get the name of those who have the same DOB as Ron, but not including Ron himself

### Part 3.3:
| Courses.Name VARCHAR(255) |
| ---- |

This query wants us to get the names of the courses that had every student enrolled in it. No course had all 4 students enrolled in it

### Part 4
Provide a relational algebra query that uses the divide operator to find the names of all students who are taking all of the 3xxx-level classes:

$\sigma (Level3kCourses, (\sigma _{cID \geq 3000 \wedge cID<4000}(Courses)))$

$\sigma (Level3kSIDs, \Pi _{cID, sID}(Courses / Level3kCourses))$

$\Pi_{Name} (\sigma_{Level3kSIDs.sID = Students.sID} (Level3kSIDs X Students) )$