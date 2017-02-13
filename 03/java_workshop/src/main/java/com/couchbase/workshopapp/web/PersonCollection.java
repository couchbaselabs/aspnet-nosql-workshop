package com.couchbase.workshopapp.web;

import java.util.ArrayList;
import java.util.List;
import com.couchbase.client.deps.com.fasterxml.jackson.annotation.JsonProperty;
import com.couchbase.workshopapp.entity.Person;


public class PersonCollection {

	@JsonProperty("persons")
	public List<Person> persons;


	public PersonCollection(Iterable<Person> persons) {
		this.persons = new ArrayList<Person>();
		for (Person p : persons) {
			this.persons.add(p);
		}
	}
}