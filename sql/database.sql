CREATE TABLE pickup_round (
    id   integer primary key,
    name text
);
CREATE TABLE team (
    id   integer primary key,
    name text
);
CREATE TABLE category (
    id      integer primary key,
    key     text,
    display text
);
CREATE TABLE person (
    id                   integer primary key,
    first_name           text,
    last_name            text,
    pickup_round_id      integer,
    team_id              integer,
    foreign key(pickup_round_id) references pickup_round(id),
    foreign key(team_id)         references team(id) 
);
CREATE TABLE settings (
    id      integer primary key,
    "key"   text,
    "value" text
);
CREATE TABLE reception_group (
    id   integer primary key,
    name text   
);
CREATE TABLE reception (
    id       integer primary key,
    name     text,
    group_nr integer not null default 0,
    reception_group_id integer,
    foreign key(reception_group_id) references reception_group(id)
);
CREATE TABLE reception_person (
    person_id    integer,
    reception_id integer,
    primary key (person_id, reception_id),
    foreign key(person_id)    references person(id),
    foreign key(reception_id) references reception(id)
);
CREATE TABLE absence (
    id         integer primary key,
    date_start date,
    date_end   date,
    person_id  integer,
    foreign key(person_id) references person(id)
);
CREATE TABLE person_category (
    person_id   integer,
    category_id integer,
    foreign key(person_id)   references person(id),
    foreign key(category_id) references category(id)
);
CREATE VIEW everyone_v as
    select p.id         as person_id
         , p.first_name as first_name
         , p.last_name  as last_name
         , t.name       as team
         , t.id         as team_id
         , pu.name      as pickup_round
         , pu.id        as pickup_round_id
         , (
         	select group_concat(c."display", ', ')
         	from person_category pc
         	inner join category c on pc.category_id = c.id
         	where pc.person_id = p.id
         ) as category
         , (
         	select group_concat(c."key", ', ')
         	from person_category pc
         	inner join category c on pc.category_id = c.id
         	where pc.person_id = p.id
         ) as category_key
    from person p   
    left join team t on t.id = p.team_id
    left join pickup_round pu on pu.id = p.pickup_round_id
    order by category_key, p.last_name
/* everyone_v(person_id,first_name,last_name,team,team_id,pickup_round,pickup_round_id,category,category_key) */;
CREATE VIEW settings_weekday_v as
    select date(value) as selected_date
        , date(date(value), (1 - strftime('%w', date(value))) || ' days') as monday
        , date(date(value), (2 - strftime('%w', date(value))) || ' days') as tuesday 
        , date(date(value), (3 - strftime('%w', date(value))) || ' days') as wednesday
        , date(date(value), (4 - strftime('%w', date(value))) || ' days') as thursday
        , date(date(value), (5 - strftime('%w', date(value))) || ' days') as friday
    from settings
/* settings_weekday_v(selected_date,monday,tuesday,wednesday,thursday,friday) */;
CREATE VIEW presence_monday_v as
    select p.*
         , rp.reception_id 
         , r.name as reception
         , r.reception_group_id        
         ,rg.name  as reception_group
    from everyone_v p 
    left join reception_person rp on p.person_id = rp.person_id
    left join reception r         on r.id        = rp.reception_id
    left join reception_group rg  on rg.id       = r.reception_group_id
    where p.person_id not in (
        select person_id
        from absence
        where date_start <= (select monday from settings_weekday_v limit 1)
        and (select monday from settings_weekday_v limit 1) <= date_end
    )
/* presence_monday_v(person_id,first_name,last_name,team,team_id,pickup_round,pickup_round_id,category,category_key,reception_id,reception,reception_group_id,reception_group) */;
CREATE VIEW presence_tuesday_v as
    select p.*
         , rp.reception_id 
         , r.name as reception
         , r.reception_group_id        
         ,rg.name  as reception_group
    from everyone_v p 
    left join reception_person rp on p.person_id = rp.person_id
    left join reception r         on r.id        = rp.reception_id
    left join reception_group rg  on rg.id       = r.reception_group_id
    where p.person_id not in (
        select person_id
        from absence
        where date_start <= (select tuesday from settings_weekday_v limit 1)
        and (select tuesday from settings_weekday_v limit 1) <= date_end
    )
/* presence_tuesday_v(person_id,first_name,last_name,team,team_id,pickup_round,pickup_round_id,category,category_key,reception_id,reception,reception_group_id,reception_group) */;
CREATE VIEW presence_wednesday_v as
    select p.*
         , rp.reception_id 
         , r.name as reception
         , r.reception_group_id        
         ,rg.name  as reception_group
    from everyone_v p 
    left join reception_person rp on p.person_id = rp.person_id
    left join reception r         on r.id        = rp.reception_id
    left join reception_group rg  on rg.id       = r.reception_group_id
    where p.person_id not in (
        select person_id
        from absence
        where date_start <= (select wednesday from settings_weekday_v limit 1)
        and (select wednesday from settings_weekday_v limit 1) <= date_end
    )
/* presence_wednesday_v(person_id,first_name,last_name,team,team_id,pickup_round,pickup_round_id,category,category_key,reception_id,reception,reception_group_id,reception_group) */;
CREATE VIEW presence_thursday_v as
    select p.*
         , rp.reception_id 
         , r.name as reception
         , r.reception_group_id        
         ,rg.name  as reception_group
    from everyone_v p 
    left join reception_person rp on p.person_id = rp.person_id
    left join reception r         on r.id        = rp.reception_id
    left join reception_group rg  on rg.id       = r.reception_group_id
    where p.person_id not in (
        select person_id
        from absence
        where date_start <= (select thursday from settings_weekday_v limit 1)
        and (select thursday from settings_weekday_v limit 1) <= date_end
    )
/* presence_thursday_v(person_id,first_name,last_name,team,team_id,pickup_round,pickup_round_id,category,category_key,reception_id,reception,reception_group_id,reception_group) */;
CREATE VIEW presence_friday_v as
    select p.*
         , rp.reception_id 
         , r.name as reception
         , r.reception_group_id        
         ,rg.name  as reception_group
    from everyone_v p 
    left join reception_person rp on p.person_id = rp.person_id
    left join reception r         on r.id        = rp.reception_id
    left join reception_group rg  on rg.id       = r.reception_group_id
    where p.person_id not in (
        select person_id
        from absence
        where date_start <= (select friday from settings_weekday_v limit 1)
        and (select friday from settings_weekday_v limit 1) <= date_end
    )
/* presence_friday_v(person_id,first_name,last_name,team,team_id,pickup_round,pickup_round_id,category,category_key,reception_id,reception,reception_group_id,reception_group) */;
CREATE VIEW presence_week_v as
    select * 
    from (
        select 1 as "day", d.*
        from presence_monday_v d
        union    
        select 2 as "day", d.*
        from presence_tuesday_v d
        union
        select 3 as "day", d.*
        from presence_wednesday_v d
        union
        select 4 as "day", d.*
        from presence_thursday_v d
        union
        select 5 as "day", d.*
        from presence_friday_v d
    )
    order by "day"              asc
           , reception_group_id asc
           , reception_id       asc
           , team               asc
           , category           asc
/* presence_week_v(day,person_id,first_name,last_name,team,team_id,pickup_round,pickup_round_id,category,category_key,reception_id,reception,reception_group_id,reception_group) */;

insert into settings (key, value) values('db_version', '1.3.0')