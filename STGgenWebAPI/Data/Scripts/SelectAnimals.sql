USE STGenetics;


--5)	Create a script to list animals older than 2 years and female, sorted by name.

SELECT * 
FROM Animal
where year(BirthDate) > 2
and sex = 'Female'
order by Name;



--6)	Create a script to list the quantity of animals by sex.


SELECT Sex , Count(sex) 
from Animal
group by Sex;

 --SELECT TOP 100 * FROM Animal