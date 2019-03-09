insert into category (key, display) values('phm', 'P.H.M.');
insert into category (key, display) values('edu', 'Educateur');
insert into category (key, display) values('drv', 'Conducteur');
/*********************************************************************/
insert into reception_group (id, name, o_val ) values (1, 'Accueil matin/soir', 0);
insert into reception_group (id, name, o_val ) values (2, 'Temps de midi', 1);
/*********************************************************************/
insert into reception (name, reception_group_id, o_val) values('Temps de midi A' , 2, 0);
insert into reception (name, reception_group_id, o_val) values('Temps de midi B' , 2, 1);
insert into reception (name, reception_group_id, o_val) values('Accueil du matin', 1, 0);
insert into reception (name, reception_group_id, o_val) values('Accueil du soir' , 1, 1);
/*********************************************************************/
insert into settings (key, value) values('week_date', date())