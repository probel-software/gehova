drop view if exists settings_weekday_v;
create view settings_weekday_v as
    select date(value) as selected_date
        , date(date(value), (1 - strftime('%w', date(value))) || ' days') as monday
        , date(date(value), (2 - strftime('%w', date(value))) || ' days') as tuesday 
        , date(date(value), (3 - strftime('%w', date(value))) || ' days') as wednesday
        , date(date(value), (4 - strftime('%w', date(value))) || ' days') as thursday
        , date(date(value), (5 - strftime('%w', date(value))) || ' days') as friday
    from settings
	where key = 'week_date';