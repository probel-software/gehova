--------------------------------------
-- TEAM
--------------------------------------
insert into team (name) values ('A');
insert into team (name) values ('B');
insert into team (name) values ('C');
insert into team (name) values ('D');
insert into team (name) values ('E');
insert into team (name) values ('F');
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
insert into person(
    id, first_name, last_name, lunchtime, reception_morning, reception_evening, pickup_round_id, team_id)
    values (1, 'Robert', 'Dupont', 0,1,2,1,1);
insert into person(
    id, first_name, last_name, lunchtime, reception_morning, reception_evening, pickup_round_id, team_id)
    values (2, 'Durant', 'Luc', 0,1,2,1,1);
insert into person(
    id, first_name, last_name, lunchtime, reception_morning, reception_evening, pickup_round_id, team_id)
    values (3, 'Zorg', 'Claude', 0,1,2,1,1);
insert into person(
    id, first_name, last_name, lunchtime, reception_morning, reception_evening, pickup_round_id, team_id)
    values (4, 'Vandeput', 'Laurent', 0,1,2,1,1);
insert into person(
    id, first_name, last_name, lunchtime, reception_morning, reception_evening, pickup_round_id, team_id)
    values (5, 'Bros', 'Mario', 0,1,2,1,1);
insert into person(
    id, first_name, last_name, lunchtime, reception_morning, reception_evening, pickup_round_id, team_id)
    values (6, 'Skywalker', 'Luke', 0,1,2,1,1);
--------------------------------------
-- ABSENCE
--------------------------------------
insert into absence(date_start, date_end, person_id) 
    values(strftime('%Y-%m-%d', '2018-10-01'), strftime('%Y-%m-%d', '2018-10-02'),1);
insert into absence(date_start, date_end, person_id) 
    values(strftime('%Y-%m-%d', '2018-10-03'), strftime('%Y-%m-%d', '2018-10-11'),1);
insert into absence(date_start, date_end, person_id) 
    values(strftime('%Y-%m-%d', '2018-10-01'), strftime('%Y-%m-%d', '2018-10-02'),2);
insert into absence(date_start, date_end, person_id) 
    values(strftime('%Y-%m-%d', '2018-10-10'), strftime('%Y-%m-%d', '2018-10-22'),3);
insert into absence(date_start, date_end, person_id) 
    values(strftime('%Y-%m-%d', '2018-10-01'), strftime('%Y-%m-%d', '2018-10-02'),4);
--------------------------------------
-- PERSON CATEGORY
--------------------------------------
insert into person_category (person_id, category_id) values(1,1);
insert into person_category (person_id, category_id) values(1,3);
insert into person_category (person_id, category_id) values(2,1);
insert into person_category (person_id, category_id) values(3,1);
insert into person_category (person_id, category_id) values(4,2);
insert into person_category (person_id, category_id) values(5,2);
insert into person_category (person_id, category_id) values(6,2);