package com.couchbase.workshopapp.service;

import com.couchbase.workshopapp.entity.Person;
import com.couchbase.workshopapp.repository.PersonRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

/**
 * Person service is the service layer implementation for crud repository.
 * It is more beneficial when working with many repositories, in our example it is
 * just a proxy interface to crud repository.
 */
@Service
public class PersonService {
	@Autowired
	PersonRepository personRepository;

	public Iterable<Person> getAll() throws Exception {
		return personRepository.findAllPeople();
	}

	public void save(Person person) throws Exception {
		personRepository.save(person);
	}

	public Person get(String id) throws Exception {
		return personRepository.findOne(id);
	}

	public void delete(String id) throws Exception {
		personRepository.delete(id);
	}

}