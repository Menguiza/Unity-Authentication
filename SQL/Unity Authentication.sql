use holamundo;

CREATE TABLE user(
id int not null auto_increment,
email varchar(100) not null,
password varchar(50) not null,
username varchar(50) not null,
score int not null,
primary key (id)
);

drop table user;