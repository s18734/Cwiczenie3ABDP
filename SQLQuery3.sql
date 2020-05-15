alter table student
add salt varchar(200)

alter table student
add refreshtoken varchar(200)


update student
set salt = 'EhYu+NZGcSRyKw13t60i1Q=='

update student
set password = '6E9yyhQV3auAOZ7yJ4qwWc6rdCkJfYrlGVhyIhg1wgI='



select * from student;
select indexNumber from student where refreshToken = 'BDC7A024-70B3-495F-B1B3-DC8B55470D7A';

