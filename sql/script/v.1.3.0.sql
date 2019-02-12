/******************************************************************************
 * Create the possibility to have more than
 * Morning reception, lunchtime and evening reception
 ******************************************************************************/
create table if not exists reception (
    id   integer primary key,
    name text
);
-------------------------------------------------------------------------------
create table if not exists reception_person (
    person_id    integer,
    reception_id integer,
    primary key (person_id, reception_id),
    foreign key(person_id)    references person(id),
    foreign key(reception_id) references reception(id)
);
-------------------------------------------------------------------------------

/* Fill receptions with default data
 */
delete from reception;
delete from reception_person;

insert into reception(id, name) values (1, 'Accueil du matin');
insert into reception(id, name) values (2, 'Temps de midi A');
insert into reception(id, name) values (3, 'Temps de midi B');
insert into reception(id, name) values (4, 'Accueil du soir');

/* Gets old data and insert it into new structure
 */
insert into reception_person (reception_id, person_id) 
    select 1  as reception_id
         , id as person_id
    from person 
    where is_reception_morning;

insert into reception_person (reception_id, person_id) 
    select 2  as reception_id
         , id as person_id
    from person
    where is_lunchtime;

insert into reception_person (reception_id, person_id) 
    select 3  as reception_id
         , id as person_id
    from person where is_reception_evening;   

/* Remove unused fields
 */
alter table person rename to person_old;

create table if not exists person (
    id                   integer primary key,
    first_name           text,
    last_name            text,
    pickup_round_id      integer,
    team_id              integer,
    foreign key(pickup_round_id) references pickup_round(id),
    foreign key(team_id)         references team(id) 
);

insert into person 
select id
     , first_name
     , last_name
     , pickup_round_id
     , team_id
from person_old;  

drop table person_old;

/******************************************************************************
 * Drop old and unused views
 ******************************************************************************/
drop view if exists all_absence_v;
drop view if exists absence_lunchtime_v;
drop view if exists absence_reception_morning_v;
drop view if exists absence_reception_evening_v;
drop view if exists absence_lunchtime_v;

/******************************************************************************
 * update database version
 ******************************************************************************/
 update settings
 set
    "value" = '1.3.0'
where "key" = 'db_version'