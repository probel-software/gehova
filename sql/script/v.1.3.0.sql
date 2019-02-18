/******************************************************************************
 * Create the possibility to have more than
 * Morning reception, lunchtime and evening reception
 ******************************************************************************/
create table if not exists reception_group (
    id   integer primary key,
    name text   
);
-------------------------------------------------------------------------------
create table if not exists reception (
    id       integer primary key,
    name     text,
    group_nr integer not null default 0,
    reception_group_id integer,
    foreign key(reception_group_id) references reception_group(id)
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

/* Fill reception groups with default data
 */
delete from reception_group;

insert into reception_group(id, name) values(1, 'Accueil matin/soir');
insert into reception_group(id, name) values(2, 'Temps de midi');

/* Fill receptions with default data
 */
delete from reception;
delete from reception_person;

insert into reception(id, name, reception_group_id) values (1, 'Accueil du matin', 1);
insert into reception(id, name, reception_group_id) values (2, 'Temps de midi A', 2);
insert into reception(id, name, reception_group_id) values (3, 'Temps de midi B', 2);
insert into reception(id, name, reception_group_id) values (4, 'Accueil du soir', 1);

/* Remove unused fields
*/
create table if not exists person_buffer (
    id                   integer primary key,
    first_name           text,
    last_name            text,
    is_lunchtime         integer not null check (is_lunchtime in (0, 1)), 
    is_reception_morning integer not null check (is_reception_morning in (0, 1)), 
    is_reception_evening integer not null check (is_reception_evening in (0, 1)), 
    pickup_round_id      integer,
    team_id              integer
);
alter table absence         rename to absence_old;
alter table person_category rename to person_category_old;

create table if not exists person (
    id                   integer primary key,
    first_name           text,
    last_name            text,
    pickup_round_id      integer,
    team_id              integer,
    foreign key(pickup_round_id) references pickup_round(id),
    foreign key(team_id)         references team(id) 
);
create table if not exists absence (
    id         integer primary key,
    date_start date,
    date_end   date,
    person_id  integer,
    foreign key(person_id) references person(id)
);
create table if not exists person_category (
    person_id   integer,
    category_id integer,
    foreign key(person_id)   references person(id),
    foreign key(category_id) references category(id)
);

/* Gets old data and insert it into new structure
 */

insert into person_buffer 
    select *
    from person;
insert into absence
    select * from absence_old;
insert into person_category
    select * from person_category_old;
/* Adapt old structure to the new one
 */    
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

insert into reception_person (reception_id, person_id) 
    select 3  as reception_id
         , id as person_id
    from person_buffer 
    where is_reception_evening;  
/* Drop buffers
 */
drop table absence_old;
drop table person_category_old;
drop table person_buffer;
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