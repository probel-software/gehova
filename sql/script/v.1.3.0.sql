/******************************************************************************
 * Create the possibility to have more than
 * Morning reception, lunchtime and evening reception
 ******************************************************************************/
/* Remove unused fields
*/
create table person_buffer (
    id                   integer primary key,
    first_name           text,
    last_name            text,
    is_lunchtime         integer not null check (is_lunchtime in (0, 1)), 
    is_reception_morning integer not null check (is_reception_morning in (0, 1)), 
    is_reception_evening integer not null check (is_reception_evening in (0, 1)), 
    pickup_round_id      integer,
    team_id              integer
);

alter table person          rename to person_old;
alter table absence         rename to absence_old;
alter table person_category rename to person_category_old;

create table person (
    id                   integer primary key,
    first_name           text,
    last_name            text,
    pickup_round_id      integer,
    team_id              integer,
    foreign key(pickup_round_id) references pickup_round(id),
    foreign key(team_id)         references team(id) 
);

create table absence (
    id         integer primary key,
    date_start date,
    date_end   date,
    person_id  integer,
    foreign key(person_id) references person(id)
);

create table person_category (
    person_id   integer,
    category_id integer,
    foreign key(person_id)   references person(id),
    foreign key(category_id) references category(id)
);

create table reception_group (
    id    integer primary key,
	o_val integer default 0, -- order value
    name  text   
);
-------------------------------------------------------------------------------
create table reception (
    id      integer primary key,
	o_val integer default 0, -- order value
    name    text,
    reception_group_id integer,
    foreign key(reception_group_id) references reception_group(id)
);
-------------------------------------------------------------------------------
create table reception_person (
    person_id    integer,
    reception_id integer,
    primary key (person_id, reception_id),
    foreign key(person_id)    references person(id),
    foreign key(reception_id) references reception(id)
);
-------------------------------------------------------------------------------
/* Gets old data and insert it into new structure
 * Fill reception groups with default data
 */
insert into reception_group(id, name, o_val) values(1, 'Accueil matin/soir', 1);
insert into reception_group(id, name, o_val) values(2, 'Temps de midi', 0);

insert into reception(id, name, reception_group_id, o_val) values (1, 'Accueil du matin', 1, 0);
insert into reception(id, name, reception_group_id, o_val) values (4, 'Accueil du soir' , 1, 1);
insert into reception(id, name, reception_group_id, o_val) values (2, 'Temps de midi A' , 2, 0);
insert into reception(id, name, reception_group_id, o_val) values (3, 'Temps de midi B' , 2, 1);

insert into person_buffer 
    select *
    from person_old;      
   
/* Adapt old structure to the new one
 */    

insert into person (id, first_name, last_name, pickup_round_id, team_id)
	select id
	     , first_name
	     , last_name
	     , pickup_round_id
	     , team_id
     from person_buffer;
	 
insert into reception_person (reception_id, person_id) 
    select 1  as reception_id
         , id as person_id
    from person_buffer 
    where is_reception_morning;

insert into reception_person (reception_id, person_id) 
    select 2  as reception_id
         , id as person_id
    from person_buffer
    where is_lunchtime;   

insert into absence
    select * from absence_old;

insert into person_category
    select * from person_category_old;	
	
/* Drop buffers
 */
delete from absence_old;
delete from person_category_old;
delete from person_buffer;
delete from person_old;

drop table absence_old;
drop table person_category_old;
drop table person_buffer;
drop table person_old;
/******************************************************************************
 * Drop old and unused views
 ******************************************************************************/
drop view if exists all_absence_v;
drop view if exists absence_lunchtime_v;
drop view if exists absence_reception_morning_v;
drop view if exists absence_reception_evening_v;
drop view if exists absence_lunchtime_v;
---
drop view if exists reception_morning_v;
drop view if exists reception_evening_v;
drop view if exists lunchtime_v;
drop view if exists pickup_rounds_v;
drop view if exists groups_v;
/******************************************************************************
 * update database version
 ******************************************************************************/
update settings
set
    "value" = '1.3.0'
where "key" = 'db_version';