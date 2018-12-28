--------------------------------------
-- CLEAR PREVIOUS DATA
--------------------------------------
delete from team;
delete from pickup_round;
delete from person;
delete from absence;
delete from person_category;
delete from settings;
--------------------------------------
-- SETTIGNS
--------------------------------------
insert into settings ("key", "value") values('week_date','2018-12-12');
--------------------------------------
-- TEAM
--------------------------------------
insert into team (name) values ('Groupe A');
insert into team (name) values ('Groupe B');
insert into team (name) values ('Groupe C');
insert into team (name) values ('Groupe D');
insert into team (name) values ('Groupe E');
insert into team (name) values ('Groupe F');
--------------------------------------
-- PICKUP ROUND
--------------------------------------
insert into pickup_round(id, name) values (1, 'RA');
insert into pickup_round(id, name) values (2, 'RB');
insert into pickup_round(id, name) values (3, 'RC');
insert into pickup_round(id, name) values (4, 'RD');
--------------------------------------
-- PERSON
--------------------------------------
insert into person(id, first_name, last_name, is_lunchtime, is_reception_morning, is_reception_evening, pickup_round_id, team_id) values (1, 'Robert' , 'Dupont'   , 0, 1, 0, 1, 1);
insert into person(id, first_name, last_name, is_lunchtime, is_reception_morning, is_reception_evening, pickup_round_id, team_id) values (2, 'Durant' , 'Luc'      , 0, 1, 1, 0, 1);
insert into person(id, first_name, last_name, is_lunchtime, is_reception_morning, is_reception_evening, pickup_round_id, team_id) values (3, 'Clause' , 'Zorg'     , 0, 1, 0, 1, 2);
insert into person(id, first_name, last_name, is_lunchtime, is_reception_morning, is_reception_evening, pickup_round_id, team_id) values (4, 'Laurent', 'Vandeput' , 0, 1, 1, 1, 2);
insert into person(id, first_name, last_name, is_lunchtime, is_reception_morning, is_reception_evening, pickup_round_id, team_id) values (5, 'Mario'  , 'Bross'    , 0, 1, 0, 1, 3);
insert into person(id, first_name, last_name, is_lunchtime, is_reception_morning, is_reception_evening, pickup_round_id, team_id) values (6, 'Luke'   , 'Skywalker', 0, 1, 1, 0, 3);
--------------------------------------
-- ABSENCE
--------------------------------------
insert into absence(date_start, date_end, person_id) values(strftime('%Y-%m-%d', '2018-10-01'), strftime('%Y-%m-%d', '2018-10-02'),1);
insert into absence(date_start, date_end, person_id) values(strftime('%Y-%m-%d', '2018-10-03'), strftime('%Y-%m-%d', '2018-10-11'),1);
insert into absence(date_start, date_end, person_id) values(strftime('%Y-%m-%d', '2018-10-01'), strftime('%Y-%m-%d', '2018-10-02'),2);
insert into absence(date_start, date_end, person_id) values(strftime('%Y-%m-%d', '2018-10-10'), strftime('%Y-%m-%d', '2018-10-22'),3);
insert into absence(date_start, date_end, person_id) values(strftime('%Y-%m-%d', '2018-10-01'), strftime('%Y-%m-%d', '2018-10-02'),4);
--------------------------------------
-- PERSON CATEGORY
--------------------------------------
insert into person_category (person_id, category_id) values(1, 1);
insert into person_category (person_id, category_id) values(1, 3);
insert into person_category (person_id, category_id) values(2, 1);
insert into person_category (person_id, category_id) values(3, 1);
insert into person_category (person_id, category_id) values(4, 2);
insert into person_category (person_id, category_id) values(5, 2);
insert into person_category (person_id, category_id) values(6, 2);