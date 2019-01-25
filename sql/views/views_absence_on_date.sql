/********************************************************************/
drop view if exists reception_morning_v;
create view reception_morning_v as
    select day_name
         , team
         , first_name
         , last_name
         , category_key
    from (
        select team
             , first_name
             , last_name
             , category_key
             , 1       as day_nr
             , 'lundi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_reception_morning = 1
            except
            select first_name
                 , last_name
                 , team
                 , category_key
            from absence_reception_morning_v
            where date_start <= (select monday from settings_weekday_v limit 1)
            and (select monday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , first_name
             , last_name 
             , category_key
             , 2       as day_nr
             , 'mardi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_reception_morning = 1
            except
            select first_name
                 , last_name
                 , team
                 , category_key
            from absence_reception_morning_v
            where date_start <= (select tuesday from settings_weekday_v limit 1)
            and (select tuesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , first_name
             , last_name 
             , category_key
             , 3       as day_nr
             , 'mercredi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_reception_morning = 1
            except
            select first_name
                 , last_name
                 , team
                 , category_key
            from absence_reception_morning_v
            where date_start <= (select wednesday from settings_weekday_v limit 1)
            and (select wednesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , first_name
             , last_name 
             , category_key
             , 4       as day_nr
             , 'jeudi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_reception_morning = 1
            except
            select first_name
                 , last_name
                 , team
                 , category_key
            from absence_reception_morning_v
            where date_start <= (select thursday from settings_weekday_v limit 1)
            and (select thursday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , first_name
             , last_name 
             , category_key
             , 5       as day_nr
             , 'vendredi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , category_key   
            from everyone_v
            where is_reception_morning = 1
            except
            select first_name
                 , last_name
                 , team
                 , category_key
            from absence_reception_morning_v
            where date_start <= (select friday from settings_weekday_v limit 1)
            and (select friday from settings_weekday_v limit 1) <= date_end
        )
   )
   where team is not null
   order by day_nr;
/********************************************************************/
drop view if exists reception_evening_v;
create view reception_evening_v as
    select day_name
         , team
         , first_name
         , last_name
         , category_key
    from (
        select team
             , first_name
             , last_name 
             , category_key
             , 1       as day_nr
             , 'lundi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_reception_evening = 1
            except
            select first_name
                 , last_name
                 , team
                 , category_key
            from absence_reception_evening_v
            where date_start <= (select monday from settings_weekday_v limit 1)
            and (select monday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , first_name
             , last_name
             , category_key 
             , 2       as day_nr
             , 'mardi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_reception_evening = 1
            except
            select first_name
                 , last_name
                 , team
                 , category_key
            from absence_reception_evening_v
            where date_start <= (select tuesday from settings_weekday_v limit 1)
            and (select tuesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , first_name
             , last_name 
             , category_key
             , 3          as day_nr
             , 'mercredi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_reception_evening = 1
            except
            select first_name
                 , last_name
                 , team
                 , category_key
            from absence_reception_evening_v
            where date_start <= (select wednesday from settings_weekday_v limit 1)
            and (select wednesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , first_name
             , last_name 
             , category_key
             , 4       as day_nr
             , 'jeudi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_reception_evening = 1
            except
            select first_name
                 , last_name
                 , team
                 , category_key
            from absence_reception_evening_v
            where date_start <= (select thursday from settings_weekday_v limit 1)
            and (select thursday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , first_name
             , last_name 
             , category_key
             , 5          as day_nr
             , 'vendredi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_reception_evening = 1
            except
            select first_name
                 , last_name
                 , team
                 , category_key
            from absence_reception_evening_v
            where date_start <= (select friday from settings_weekday_v limit 1)
            and (select friday from settings_weekday_v limit 1) <= date_end
        )
   )
   where team is not null
   order by day_nr;
/********************************************************************/
drop view if exists lunchtime_v;
create view lunchtime_v as
    select day_name
         , team
         , first_name
         , last_name  
         , category_key       
    from (
        select team
             , first_name
             , last_name 
             , category_key
             , 1       as day_nr
             , 'lundi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_lunchtime = 1
            except
            select first_name
                 , last_name
                 , team
                 , category_key
            from absence_lunchtime_v
            where date_start <= (select monday from settings_weekday_v limit 1)
            and (select monday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , first_name          
             , last_name 
             , category_key
             , 2       as day_nr
             , 'mardi' as day_name 
        from (
            select first_name          
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_lunchtime = 1
            except
            select first_name          
                 , last_name
                 , team
                 , category_key
            from absence_lunchtime_v
            where date_start <= (select tuesday from settings_weekday_v limit 1)
            and (select tuesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , first_name          
             , last_name 
             , category_key
             , 3          as day_nr
             , 'mercredi' as day_name 
        from (
            select first_name          
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_lunchtime = 1
            except
            select first_name          
                 , last_name
                 , team
                 , category_key
            from absence_lunchtime_v
            where date_start <= (select wednesday from settings_weekday_v limit 1)
            and (select wednesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , first_name          
             , last_name 
             , category_key
             , 4       as day_nr
             , 'jeudi' as day_name 
        from (
            select first_name          
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_lunchtime = 1
            except
            select first_name          
                 , last_name
                 , team
                 , category_key
            from absence_lunchtime_v
            where date_start <= (select thursday from settings_weekday_v limit 1)
            and (select thursday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , first_name          
             , last_name 
             , category_key
             , 5          as day_nr
             , 'vendredi' as day_name 
        from (
            select first_name          
                 , last_name
                 , team
                 , category_key
            from everyone_v
            where is_lunchtime = 1
            except
            select first_name          
                 , last_name
                 , team
                 , category_key
            from absence_lunchtime_v
            where date_start <= (select friday from settings_weekday_v limit 1)
            and (select friday from settings_weekday_v limit 1) <= date_end
        )
   )
   where team is not null
   order by day_nr;
/********************************************************************/
drop view if exists pickup_rounds_v;
create view pickup_rounds_v as
    select day_name
         , team
         , pickup_round
         , first_name
         , last_name         
         , category_key
    from (
        select team
             , pickup_round
             , first_name
             , last_name 
             , category_key
             , 1       as day_nr
             , 'lundi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from everyone_v
            except
            select first_name
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from all_absence_v
            where date_start <= (select monday from settings_weekday_v limit 1)
            and (select monday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , pickup_round
             , first_name          
             , last_name 
             , category_key
             , 2       as day_nr
             , 'mardi' as day_name 
        from (
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from everyone_v
            except
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from all_absence_v
            where date_start <= (select tuesday from settings_weekday_v limit 1)
            and (select tuesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , pickup_round
             , first_name          
             , last_name 
             , category_key
             , 3          as day_nr
             , 'mercredi' as day_name 
        from (
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from everyone_v
            except
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from all_absence_v
            where date_start <= (select wednesday from settings_weekday_v limit 1)
            and (select wednesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , pickup_round
             , first_name          
             , last_name 
             , category_key
             , 4       as day_nr
             , 'jeudi' as day_name 
        from (
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from everyone_v
            except
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from all_absence_v
            where date_start <= (select thursday from settings_weekday_v limit 1)
            and (select thursday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , pickup_round
             , first_name          
             , last_name 
             , category_key
             , 5          as day_nr
             , 'vendredi' as day_name 
        from (
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from everyone_v
            except
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from all_absence_v
            where date_start <= (select friday from settings_weekday_v limit 1)
            and (select friday from settings_weekday_v limit 1) <= date_end
        )
   )
   where team is not null
   and pickup_round is not null
   order by day_nr;
/********************************************************************/
drop view if exists groups_v;
create view groups_v as
    select day_name
         , team
         , pickup_round
         , first_name
         , last_name         
         , category_key
    from (
        select team
             , pickup_round
             , first_name
             , last_name 
             , category_key
             , 1       as day_nr
             , 'lundi' as day_name 
        from (
            select first_name
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from everyone_v
            except
            select first_name
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from all_absence_v
            where date_start <= (select monday from settings_weekday_v limit 1)
            and (select monday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , pickup_round
             , first_name          
             , last_name 
             , category_key
             , 2       as day_nr
             , 'mardi' as day_name 
        from (
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from everyone_v
            except
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from all_absence_v
            where date_start <= (select tuesday from settings_weekday_v limit 1)
            and (select tuesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , pickup_round
             , first_name          
             , last_name 
             , category_key
             , 3          as day_nr
             , 'mercredi' as day_name 
        from (
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from everyone_v
            except
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from all_absence_v
            where date_start <= (select wednesday from settings_weekday_v limit 1)
            and (select wednesday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , pickup_round
             , first_name          
             , last_name 
             , category_key
             , 4       as day_nr
             , 'jeudi' as day_name 
        from (
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from everyone_v
            except
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from all_absence_v
            where date_start <= (select thursday from settings_weekday_v limit 1)
            and (select thursday from settings_weekday_v limit 1) <= date_end
        )
        union
        select team
             , pickup_round
             , first_name          
             , last_name 
             , category_key
             , 5          as day_nr
             , 'vendredi' as day_name 
        from (
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from everyone_v
            except
            select first_name          
                 , last_name
                 , team
                 , pickup_round
                 , category_key
            from all_absence_v
            where date_start <= (select friday from settings_weekday_v limit 1)
            and (select friday from settings_weekday_v limit 1) <= date_end
        )
   )
   where team is not null
   order by day_nr;
