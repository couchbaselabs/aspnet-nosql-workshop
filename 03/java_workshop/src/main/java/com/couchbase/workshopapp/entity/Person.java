package com.couchbase.workshopapp.entity;

import com.couchbase.client.java.repository.annotation.Id;
import org.springframework.data.annotation.Version;

/**
 * Person entity for our person repository
 */
public class Person {
	/* Couchbase key */
	@Id
	public String id;

	/* Couchbase document cas */
	// commented out CAS related code
	//@Version
	//public long Version;

	public String firstname;

	public String lastname;

	public String email;

	@Override
	public String toString(){
		return  "firstname:" + this.firstname + "," +
				"lastname:" + this.lastname + "," +
				"email:" + this.email; // + "," +
				// commented out CAS related code
				//"cas:" + this.Version;
	}

}