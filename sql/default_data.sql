insert into category (key, display) values('phm', 'P.H.M.');
insert into category (key, display) values('edu', 'Educateur');
insert into category (key, display) values('drv', 'Conducteur');
/*********************************************************************/
insert into reception_group (id, name ) values (1, 'Accueil matin/soir');
insert into reception_group (id, name ) values (2, 'Temps de midi');
/*********************************************************************/
insert into reception (name, reception_group_id) values('Temps de midi A', 2);
insert into reception (name, reception_group_id) values('Temps de midi B', 2);
insert into reception (name, reception_group_id) values('Accueil du matin', 1);
insert into reception (name, reception_group_id) values('Accueil du soir', 1);
/*********************************************************************/
insert into settings (key, value) values('week_date', date())