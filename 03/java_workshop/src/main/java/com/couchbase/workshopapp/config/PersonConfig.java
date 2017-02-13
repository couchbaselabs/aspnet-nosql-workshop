package com.couchbase.workshopapp.config;

import com.couchbase.workshopapp.service.PersonService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.couchbase.repository.config.EnableCouchbaseRepositories;


@Configuration
// TODO: add an attribute to enable couchbase repositories
public class PersonConfig {
	/**
	 * Create a bean for person service
	 * Returns the service instance
	 */
	@Bean
	public PersonService personService() {
		return new PersonService();
	}
}