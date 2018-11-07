create table if not exists pickup_round (
    id   integer primary key,
    name text
);

create table if not exists team (
    id integer primary key,
    name text
);

create table if not exists category (
    id integer primary key,
    key     text,
    display text
);

create table if not exists person (
    id integer primary key,
    first_name        text,
    last_name         text,
    lunchtime         integer,
    reception_morning integer,
    reception_evening integer,
    pickup_round_id   integer,
    team_id           integer,
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
)