USE CuyahogaECommerceDemov2
DECLARE @SITE_ID int
SET @SITE_ID =  12


				DELETE cuyahoga_noderole
				FROM cuyahoga_noderole nr
				INNER JOIN cuyahoga_node n ON nr.nodeid = n.nodeid
				where n.siteId = @SITE_ID

			

				DELETE cuyahoga_node
				FROM cuyahoga_node  n
				where n.siteId = @SITE_ID


				delete cuyahoga_sitealias 
				from cuyahoga_sitealias sa
				where sa.siteId = @SITE_ID

	delete cuyahoga_site
				from cuyahoga_site s
				where s.siteId = @SITE_ID

				delete cuyahoga_template
				from cuyahoga_template t
				where t.name = 'ECommerceTemplate'

			

			

Go